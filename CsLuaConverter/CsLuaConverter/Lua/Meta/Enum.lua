

local Enum = function(table, name, namespace, signatureHash)
    table.__typeof = System.Type(name, namespace, System.Object.__typeof, 0, nil, nil, table, 'Enum', signatureHash);

    return table;
end

_M.EN = Enum;