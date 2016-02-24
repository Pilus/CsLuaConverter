

local Enum = function(table, name, namespace)
    table.__typeof = System.Type(name, namespace, System.Object.__typeof, 0, nil, nil, table, 'Enum');

    return table;
end

_M.EN = Enum;