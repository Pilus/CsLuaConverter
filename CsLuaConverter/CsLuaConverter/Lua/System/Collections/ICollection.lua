System.Collections.ICollection = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local implements = {
        System.Collections.IEnumerable.__typeof,
    };
    local typeObject = System.Type('ICollection','System.Collections', nil, 0, generics, implements, interactionElement, 'Interface');
    return 'Interface', typeObject, nil, nil, nil;
end})