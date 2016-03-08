 
--============= Argument Matching =============
local ScoreArguments = function(expectedTypes, argTypes, args, isParams)
    local sum = 0;
    local str = "";
    local foldOutArray = false;

    for i = 1, argTypes.num do
        local argType = argTypes[i];
        
        if (argType) then
            local expectedType = expectedTypes[i] or (isParams and expectedTypes[#(expectedTypes)]);
            if (expectedType == nil) then
                return nil, ", - Skipping candidate. Did not have enough expected types. Expected "..#(expectedTypes).." had: "..#(argTypes);
            end
        
            str = str .. ", " .. expectedType.ToString();

            local score = argType.GetMatchScore(expectedType, args[i]);

            if (isParams and i == #(expectedTypes) and i == argTypes.num) and argType.FullName == "System.Array" then
                local arrayGenricType = argType.GetGenericArguments()[0];
                local foldOutScore = arrayGenricType.GetMatchScore(expectedType, (args[i] % _M.DOT)[0]);
                if foldOutScore >= score then
                    score = foldOutScore;
                    foldOutArray = true;
                end
            end

            if (score == nil) then
                local additional = ""
                for j = i + 1, #(expectedTypes) do
                    additional = ", " .. expectedTypes[j].ToString();
                end

                return nil, str .. additional .. " - Skipping candidate. Arg did not match "..argType.ToString();
            end
            sum = sum + score;
        end
    end
    
    return sum, str, foldOutArray;
end

local SelectMatchingByTypes = function(list, args, name, methodGenerics)
    assert(type(list) == "table", "Expected a table as 1th argument to _M.AM, got "..type(list))
    assert(type(args) == "table", "Expected a table as 2th argument to _M.AM, got "..type(args))
    assert(type(name) == "string", "Expected a string as 3rd argument to _M.AM, got "..type(args))
    assert(methodGenerics == nil or type(methodGenerics) == "table", "Expected a table as 4th argument to _M.AM, got "..type(args))
    local argTypes = {num = #(args)};
    local argTypeStr = "";

    for i=1,#(args) do
        local arg = args[i];

        if not(arg == nil) then
            argTypes[i] = (arg%_M.DOT).GetType();
            argTypeStr = argTypeStr .. "," .. argTypes[i].FullName;
        else
            argTypeStr = argTypeStr .. ",null";
        end
    end
    
    local bestMatch, bestScore, bestFoldOutArray;
    local candidatesStr = "Candidates:";

    for _, element in ipairs(list) do
        local types = {};
        for _, t in ipairs(element.types) do 
            if type(t) == "string" then
                if methodGenerics and methodGenerics[element.generics[t]] then
                    table.insert(types, methodGenerics[element.generics[t]]);
                else
                    table.insert(types, System.Object.__typeof);
                end
            else
                table.insert(types, t);
            end
        end

        local score, scoreStr, foldOutArray = ScoreArguments(types, argTypes, args, element.isParams);
        candidatesStr = candidatesStr .. "\n  " .. scoreStr;

        if not(score == nil) and (not(bestScore) or score > bestScore) then
            bestMatch = element;
            bestScore = score;
            bestFoldOutArray = foldOutArray;
        end
    end
    
    if not(bestMatch) then
        error(string.format("No signature match found for method (%s).\nArgs: %s.\n%s", name, argTypeStr, candidatesStr));
    end
    
    return bestMatch, bestFoldOutArray;
end

_M.AM = SelectMatchingByTypes;

local GetAllMembers = function(members, implements)
    for _, implement in pairs(implements) do
        local _, interfaceMembers = implement.interactionElement.__meta({});

        for name, memberPair in pairs(interfaceMembers) do
            for _, member in pairs(memberPair) do
                _M.IM(members, name, member);
            end 
        end
    end
end
_M.GAM = GetAllMembers;