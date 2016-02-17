 
--============= Argument Matching =============
local ScoreArguments = function(expectedTypes, argTypes, args, isParams)
    local sum = 0;
    local str = "";

    for i,argType in ipairs(argTypes) do
        local expectedType = expectedTypes[i] or (isParams and expectedTypes[#(expectedTypes)]);
        if (expectedType == nil) then
            return nil, ", - Skipping candidate. Did not have enough expected types. Expected "..#(expectedTypes).." had: "..#(argTypes);
        end
        
        str = str .. "," .. expectedType.ToString();

        local score = argType.GetMatchScore(expectedType, args[i]);
        if (score == nil) then
            local additional = ""
            for j = i + 1, #(expectedTypes) do
                additional = ", " .. expectedTypes[j].ToString();
            end

            return nil, str .. additional .. " - Skipping candidate. Arg did not match "..argType.ToString();
        end
        sum = sum + score;
    end
    
    return sum, str;
end

local SelectMatchingByTypes = function(list, args, methodGenerics)
    assert(type(list) == "table", "Expected a table as 1th argument to _M.AM, got "..type(list))
    assert(type(args) == "table", "Expected a table as 2th argument to _M.AM, got "..type(args))
    local argTypes = {};
    local argTypeStr = "";

    for i,arg in ipairs(args) do
        argTypes[i] = (arg%_M.DOT).GetType();
        argTypeStr = argTypeStr .. "," .. argTypes[i].FullName;
    end
    
    local bestMatch, bestScore;
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

        local score, scoreStr = ScoreArguments(types, argTypes, args, element.isParams);
        candidatesStr = candidatesStr .. "\n  " .. scoreStr;

        if not(score == nil) and (not(bestScore) or score > bestScore) then
            bestMatch = element;
            bestScore = score;
        end
    end
    
    if not(bestMatch) then
        error(string.format("No match found.\nArgs: %s.\n%s", argTypeStr, candidatesStr));
    end
    
    return bestMatch;
end

_M.AM = SelectMatchingByTypes;