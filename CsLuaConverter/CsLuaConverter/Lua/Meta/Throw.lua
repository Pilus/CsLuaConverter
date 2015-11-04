
_M.Throw = function(exception)
    if not(System.Exception.__is(exception)) then
        error("Non exception thrown.");
    end

    _M._CurrentException = exception;
    error((exception % _M.DOT).ToString(), 2);
end