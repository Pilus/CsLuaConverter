
System = System or {};
System.Object = _M.NE({[0] = function()
	local typeObject = System.Type('Object','System');
	local nonStatics = {
		-- TOOD: Add non static object members
	};
	return  typeObject, {}, nonStatics, {}, function() return {}; end;
end})
