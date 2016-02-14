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
    if not(defaultValues[type] == nil) then
        return defaultValues[type];
    end

    if type.IsEnum then
        return type.InteractionElement.__default;
    end
end
_M.DV = GetDefaultValue;
