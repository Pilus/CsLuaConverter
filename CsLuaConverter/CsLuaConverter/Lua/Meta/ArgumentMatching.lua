 
--============= Argument Matching =============

local ScoreArguments = function(expectedTypes, argTypes)
    local sum = 0;
    for i,argType in ipairs(argTypes) do
        local expectedType = expectedTypes[i];
        if not(expectedType) then
            return nil;
        end
        
        local score = expectedType.GetMatchScore(argType);
        if not(score) then
            return nil;
        end
        sum = sum + score;
    end
    
    return sum;
end

local SelectMatchingByTypes = function(list, args)
    assert(type(list) == "table", "Expected a table as 1th argument to M.AM, got "..type(list))
    assert(type(args) == "table", "Expected a table as 2th argument to M.AM, got "..type(args))
    local argTypes = {};
    for i,arg in ipairs(args) do
        argTypes[i] = (arg%_M.DOT(nil)).GetType();
    end
    
    local bestMatch, bestScore;
    for _, element in ipairs(list) do
        local score = ScoreArguments(element.types, argTypes);
        
        if not(bestScore) or score > bestScore then
            bestMatch = element;
            bestScore = score;
        end
    end
    
    if not(bestMatch) then
        error("No match found");
    end
    
    return bestMatch;
end

_M = _M or {};
_M.AM = SelectMatchingByTypes;