local defaultValues;

local initializeDefaultValues = function()
    if defaultValues then 
        return 
    end

    defaultValues = {
        [System.Int32.__typeof] = 0,
        [System.Boolean.__typeof] = false,
    };
end


local GetDefaultValue = function(type)
    initializeDefaultValues();
    return defaultValues[type];
end
_M.DV = GetDefaultValue;
