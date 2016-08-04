

Lua.Table = {
    getn = table.getn,
    Foreach = function(t, iterator) return table.Foreach(t, iterator[2].innerAction); end,
    foreachi = function(t, iterator) return table.Foreach(t, iterator[2].innerAction); end,
    sort = function(t, iterator)
        if iterator then
            return table.Foreach(t, iterator[2].innerAction); 
        end
        return table.Foreach(t); 
    end,
    contains = table.contains,
    insert = table.insert,
    remove = table.remove,
    wipe = table.wipe,
};