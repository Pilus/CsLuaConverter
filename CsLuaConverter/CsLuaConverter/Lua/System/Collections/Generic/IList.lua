System.Collections.Generic.IList = _M.NE({[0] = function(interactionElement, generics, staticValues)
    local implements = {
        System.Collections.ICollection.__typeof,
        System.Collections.IEnumerable.__typeof,
    };
    local typeObject = System.Type('IList','System.Collections.Generic', baseTypeObject, 0, generics, implements, interactionElement, 'Interface');
    return 'Interface', typeObject, nil, nil, nil;
end})