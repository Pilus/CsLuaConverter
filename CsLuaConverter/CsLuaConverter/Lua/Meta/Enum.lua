

local Enum = function(table, name, namespace)

    for _,v in pairs(table) do
        table.__default = v;
        break;
    end

    table.__typeof = System.Type(name, namespace, System.Object.__typeof, 0, nil, nil, table, 'Enum');

    return table;
end

_M.EN = Enum;