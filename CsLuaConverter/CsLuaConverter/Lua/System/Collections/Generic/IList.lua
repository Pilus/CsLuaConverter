System.Collections.Generic.IList = _M.NE({[1] = function(interactionElement, generics, staticValues)
    local implements = {
        System.Collections.ICollection.__typeof,
        System.Collections.IEnumerable.__typeof,
    };
    local typeObject = System.Type('IList','System.Collections.Generic', nil, 0, generics, implements, interactionElement, 'Interface', 2980);
    return 'Interface', typeObject, nil, nil, nil;
end})