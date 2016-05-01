
local lambda = function(f, retArg, ...)
    
    if (retArg == nil) then
        return System.Action[{...}](f);
    end

    return System.Func[{..., retArg}](f);
end

_M.LB = lambda;
