
_M.Try = function(try, catch, finally)
    _M._CurrentException = nil;
    local success, err = pcall(try)
    local exception = _M._CurrentException;
    _M._CurrentException = nil;

    if not(success) then
        exception = exception or System.Exception("Lua error:\n" .. (err or "nil"));
        
        local matchFound = false;
        for _, catchCase in ipairs(catch or {}) do
            if catchCase.type == nil or catchCase.type.interactionElement.__is(exception) then
                catchCase.func(exception)
                matchFound = true;
                break;
            end
        end

        if (matchFound == false) then -- rethrow
            if finally then
                finally();
            end

            __CurrentException = exception;
            error(err, 2);
        end
    end

    if finally then
        finally();
    end
end