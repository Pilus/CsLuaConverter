
System = System or {};
System.String = _M.NE({[0] = function()
	local typeObject = System.Type('String','System');
	local nonStatics = {
		-- TOOD: Add non static object members
	};
	return  typeObject, {}, nonStatics, {}, function() return {}; end;
end})
