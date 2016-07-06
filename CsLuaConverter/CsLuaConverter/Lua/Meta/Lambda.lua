
local lambda = function(f, retArg, ...)
    
    if (retArg == nil) then
        return System.Action[{...}]._C_0_0(f);
    end

    return System.Func[{..., retArg}](f);
end

_M.LB = lambda;
