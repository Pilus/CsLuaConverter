using CsLuaFramework.Attributes;

[assembly:CsLuaLibrary]

namespace BlizzardApi.Global
{
    using System;
    using CsLuaFramework.Wrapping;
    using Lua;

    public static class Global
    {
        private static IApi api;
        private static IFrames frames;
        private static IFrameProvider frameProvider;

        public static IWrapper Wrapper = new Wrapper();

        public static IApi Api
        { 
            get
            {
                if (api == null)
                {
                    api = Wrapper.Wrap<IApi>("_G");
                }
                return api;
            }
            set
            {
                api = value;
            }
        }
        public static IFrames Frames
        {
            get
            {
                if (frames == null)
                {
                    frames = Wrapper.Wrap<IFrames>("_G");
                }
                return frames;
            }
            set
            {
                frames = value;
            }
        }

        public static IFrameProvider FrameProvider
        {
            get
            {
                if (frameProvider == null)
                {
                    frameProvider = Wrapper.Wrap<IFrameProvider>("_G");
                }
                return frameProvider;
            }
            set
            {
                frameProvider = value;
            }
        }

        private static string FrameTypeTranslator(NativeLuaTable obj) // TODO: Give this to the frame Wrapper
        {
            if (obj["GetObjectType"] != null)
            {
                return "BlizzardApi.WidgetInterfaces.I" + (obj["GetObjectType"] as Func<NativeLuaTable, string>)(obj);
            }

            return null;
        }
    }
}