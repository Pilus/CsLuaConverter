
System.Collections.Generic.List = _M.NE({[1] = function(interactionElement, generics, staticValues)
    local implements = {};
    local baseTypeObject, members = System.Object.__meta(staticValues);
    local typeObject = System.Type('List','System.Collections.Generic',baseTypeObject,1,generics,implements,interactionElement);

    _M.IM(members,'ForEach',{
        level = typeObject.Level,
        memberType = 'Method',
        scope = 'Public',
        types = {System.Action[{generics[1]}].__typeof},
        func = function(element,action)
            for i = 1,#(element[2]) do
                (action%_M.DOT)(element[2][i]);
            end
        end,
    });

    local constructors = {
        {
            types = {},
            func = function() end,
        }
    };

    local initialize = function(element, values)
        for i=1,#(values) do
            element[2][i] = values[i];
        end
    end

    return "Class", typeObject, members, constructors, function() return {[1] = {},[2] = {}, ["type"] = typeObject}; end, implements, initialize;
end})
