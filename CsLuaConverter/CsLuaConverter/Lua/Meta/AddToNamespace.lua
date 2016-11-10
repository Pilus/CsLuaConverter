
_M.ATN = function(fullNamespace, name, element)
    local namespace = {string.split(".", fullNamespace)};
    local t = _G;

    for _,v in ipairs(namespace) do
        if t[v] == nil then
            t[v] = {};
        end

        t = t[v];
    end

    t[name] = element;
end
