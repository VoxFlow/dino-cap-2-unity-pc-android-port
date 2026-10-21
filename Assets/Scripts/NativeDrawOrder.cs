namespace Dc2Pilot
{
    /// <summary>CAObjCallBack::CAObjCallBack 0xaff4 draw-order allocator (NATIVE): every CAObj-derived object adds 50 to the static DrawOrerIndex (0x23d100, written only there, start value 0) and keeps it at +76;
    /// CAObjCallBack::OnUpdate 0xa6cc gives child sprite i DrawOrder = i + that base (Sprite+88) and the layer at +92 (default 10; set by the ctor, not overridden for hero or dinosaurs).
    /// RenderManager::DoRenderSprite sorts layers 5, 10 and 12 by DrawOrder (SetLayerMode(.,5) in every scene OnLoad), so sprites of objects created later draw above earlier ones.
    /// Units here are creation indices (native base = index * 50). The counter is reset per gameplay scene, which does not change the relative order inside one scene.</summary>
    public static class NativeDrawOrder
    {
        static int index;
        public static void Reset() { index = 0; }
        public static int Allocate() { return ++index; }
        public const int Stride = 50;
        /// <summary>Native DrawOrder base of creation index i (CAObj +76).</summary>
        public static int Base(int index) { return index * Stride; }
        /// <summary>Unity SortingGroup order for a CAObj object with DrawOrder base b in its render-layer root. Plain sprites of the layer keep the Sprite ctor DrawOrder 0
        /// (port renderer orders stay below 200: background children = layer number, items/nest 100), so every CAObj draws above them in layers 5/10/12.</summary>
        public static int CaGroup(int drawBase) { return 200 + drawBase; }
        /// <summary>RenderManager::DoRender 0x172d90 draws custom draws (CompAnimObj: bosses, effectClassN, egg truck) after every sprite of the same layer.</summary>
        public const int CustomDraw = 30000;
    }
}
