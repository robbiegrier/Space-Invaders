using System;
using System.Diagnostics;
using System.Xml;

namespace SpaceInvaders
{
    // Manager that contains individual glyphs for potentially many glyph families.
    // Must look up a glyph by both family name and ascii key
    internal class GlyphMan : ManBase
    {
        private GlyphMan(int reserveNum = 3, int reserveGrow = 1)
            : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)
        {
            poNodeCompare = new Glyph();
        }

        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);
            Debug.Assert(pInstance == null);

            if (pInstance == null)
            {
                pInstance = new GlyphMan(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
        }

        public static Glyph Add(Glyph.Name name, int key, Texture.Name textName, float x, float y, float width, float height)
        {
            return privGetInstance().privAdd(name, key, textName, x, y, width, height);
        }

        // find a glyph by family name and ascii key
        public static Glyph Find(Glyph.Name glyphFamily, int asciiKey)
        {
            return privGetInstance().privFind(glyphFamily, asciiKey);
        }

        public static void Remove(Glyph pImage)
        {
            privGetInstance().privRemove(pImage);
        }

        public static void Dump()
        {
            privGetInstance().privDump();
        }

        private Glyph privAdd(Glyph.Name name, int key, Texture.Name textName, float x, float y, float width, float height)
        {
            GlyphMan pMan = GlyphMan.privGetInstance();

            Glyph pNode = (Glyph)pMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, key, textName, x, y, width, height);
            return pNode;
        }

        // manually load the space invaders font.
        public static void AddSpaceInvaders()
        {
            Texture.Name textName = Texture.Name.SpaceInvaders;
            Glyph.Name glyphName = Glyph.Name.SpaceInvaders;

            int key = 65;
            GlyphMan.Add(glyphName, key++, textName, 3, 36, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 11, 36, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 19, 36, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 27, 36, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 35, 36, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 43, 36, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 51, 36, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 59, 36, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 67, 36, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 75, 36, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 83, 36, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 91, 36, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 99, 36, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 3, 46, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 11, 46, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 19, 46, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 27, 46, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 35, 46, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 43, 46, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 51, 46, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 59, 46, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 67, 46, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 75, 46, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 83, 46, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 91, 46, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 99, 46, 5, 7);

            key = 48;
            GlyphMan.Add(glyphName, key++, textName, 3, 56, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 11, 56, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 19, 56, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 27, 56, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 35, 56, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 43, 56, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 51, 56, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 59, 56, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 67, 56, 5, 7);
            GlyphMan.Add(glyphName, key++, textName, 75, 56, 5, 7);

            GlyphMan.Add(glyphName, 60, textName, 83, 56, 5, 7);
            GlyphMan.Add(glyphName, 62, textName, 91, 56, 5, 7);
            GlyphMan.Add(glyphName, 32, textName, 99, 56, 5, 7);
            GlyphMan.Add(glyphName, 61, textName, 107, 56, 5, 7);
            GlyphMan.Add(glyphName, 42, textName, 115, 56, 5, 7);
            GlyphMan.Add(glyphName, 63, textName, 123, 56, 5, 7);
            GlyphMan.Add(glyphName, 45, textName, 131, 56, 5, 7);

            // add special chars here (coded as lowercase a-z)
            key = 97;
            GlyphMan.Add(glyphName, key++, textName, 3, 3, 12, 8); // octo
            GlyphMan.Add(glyphName, key++, textName, 33, 3, 11, 8); // crab
            GlyphMan.Add(glyphName, key++, textName, 61, 3, 8, 8); // squid
            GlyphMan.Add(glyphName, key++, textName, 99, 3, 16, 8); // saucer
            GlyphMan.Add(glyphName, key++, textName, 3, 14, 13, 8); // player

            //ImageMan.Add(Image.Name.OctopusA, Texture.Name.SpaceInvaders, 3, 3, 12, 8);
            //ImageMan.Add(Image.Name.CrabA, Texture.Name.SpaceInvaders, 33, 3, 11, 8);
            //ImageMan.Add(Image.Name.SquidA, Texture.Name.SpaceInvaders, 61, 3, 8, 8);
            //ImageMan.Add(Image.Name.Saucer, Texture.Name.SpaceInvaders, 99, 3, 16, 8);
            //ImageMan.Add(Image.Name.Player, Texture.Name.SpaceInvaders, 3, 14, 13, 8);

            //Add(Image.Name.A, Texture.Name.SpaceInvaders, 3, 36, 5, 7);
            //Add(Image.Name.B, Texture.Name.SpaceInvaders, 11, 36, 5, 7);
            //Add(Image.Name.C, Texture.Name.SpaceInvaders, 19, 36, 5, 7);
            //Add(Image.Name.D, Texture.Name.SpaceInvaders, 27, 36, 5, 7);
            //Add(Image.Name.E, Texture.Name.SpaceInvaders, 35, 36, 5, 7);
            //Add(Image.Name.F, Texture.Name.SpaceInvaders, 43, 36, 5, 7);
            //Add(Image.Name.G, Texture.Name.SpaceInvaders, 51, 36, 5, 7);
            //Add(Image.Name.H, Texture.Name.SpaceInvaders, 59, 36, 5, 7);
            //Add(Image.Name.I, Texture.Name.SpaceInvaders, 67, 36, 5, 7);
            //Add(Image.Name.J, Texture.Name.SpaceInvaders, 75, 36, 5, 7);
            //Add(Image.Name.K, Texture.Name.SpaceInvaders, 83, 36, 5, 7);
            //Add(Image.Name.L, Texture.Name.SpaceInvaders, 91, 36, 5, 7);
            //Add(Image.Name.M, Texture.Name.SpaceInvaders, 99, 36, 5, 7);
            //Add(Image.Name.N, Texture.Name.SpaceInvaders, 3, 46, 5, 7);
            //Add(Image.Name.O, Texture.Name.SpaceInvaders, 11, 46, 5, 7);
            //Add(Image.Name.P, Texture.Name.SpaceInvaders, 19, 46, 5, 7);
            //Add(Image.Name.Q, Texture.Name.SpaceInvaders, 27, 46, 5, 7);
            //Add(Image.Name.R, Texture.Name.SpaceInvaders, 35, 46, 5, 7);
            //Add(Image.Name.S, Texture.Name.SpaceInvaders, 43, 46, 5, 7);
            //Add(Image.Name.T, Texture.Name.SpaceInvaders, 51, 46, 5, 7);
            //Add(Image.Name.U, Texture.Name.SpaceInvaders, 59, 46, 5, 7);
            //Add(Image.Name.V, Texture.Name.SpaceInvaders, 67, 46, 5, 7);
            //Add(Image.Name.W, Texture.Name.SpaceInvaders, 75, 46, 5, 7);
            //Add(Image.Name.X, Texture.Name.SpaceInvaders, 83, 46, 5, 7);
            //Add(Image.Name.Y, Texture.Name.SpaceInvaders, 91, 46, 5, 7);
            //Add(Image.Name.Z, Texture.Name.SpaceInvaders, 99, 46, 5, 7);

            //Add(Image.Name.Zero, Texture.Name.SpaceInvaders, 3, 56, 5, 7);
            //Add(Image.Name.One, Texture.Name.SpaceInvaders, 11, 56, 5, 7);
            //Add(Image.Name.Two, Texture.Name.SpaceInvaders, 19, 56, 5, 7);
            //Add(Image.Name.Three, Texture.Name.SpaceInvaders, 27, 56, 5, 7);
            //Add(Image.Name.Four, Texture.Name.SpaceInvaders, 35, 56, 5, 7);
            //Add(Image.Name.Five, Texture.Name.SpaceInvaders, 43, 56, 5, 7);
            //Add(Image.Name.Six, Texture.Name.SpaceInvaders, 51, 56, 5, 7);
            //Add(Image.Name.Seven, Texture.Name.SpaceInvaders, 59, 56, 5, 7);
            //Add(Image.Name.Eight, Texture.Name.SpaceInvaders, 67, 56, 5, 7);
            //Add(Image.Name.Nine, Texture.Name.SpaceInvaders, 75, 56, 5, 7);

            //Add(Image.Name.LessThan, Texture.Name.SpaceInvaders, 83, 56, 5, 7);
            //Add(Image.Name.GreaterThan, Texture.Name.SpaceInvaders, 91, 56, 5, 7);
            //Add(Image.Name.Space, Texture.Name.SpaceInvaders, 99, 56, 5, 7);
            //Add(Image.Name.Equals, Texture.Name.SpaceInvaders, 107, 56, 5, 7);
            //Add(Image.Name.Asterisk, Texture.Name.SpaceInvaders, 115, 56, 5, 7);
            //Add(Image.Name.Question, Texture.Name.SpaceInvaders, 123, 56, 5, 7);
            //Add(Image.Name.Hyphen, Texture.Name.SpaceInvaders, 131, 56, 5, 7);
        }

        public static void AddXml(String assetName, Glyph.Name glyphName, Texture.Name textName)
        {
            System.Xml.XmlTextReader reader = new XmlTextReader(assetName);

            int key = -1;
            int x = -1;
            int y = -1;
            int width = -1;
            int height = -1;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.GetAttribute("key") != null)
                        {
                            key = Convert.ToInt32(reader.GetAttribute("key"));
                        }
                        else if (reader.Name == "x")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    x = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "y")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    y = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "width")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    width = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "height")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    height = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        break;

                    case XmlNodeType.EndElement:
                        if (reader.Name == "character")
                        {
                            // Debug.WriteLine("key:{0} x:{1} y:{2} w:{3} h:{4}", key, x, y, width, height);
                            GlyphMan.Add(glyphName, key, textName, x, y, width, height);
                        }
                        break;
                }
            }

            // Debug.Write("\n");
        }

        private Glyph privFind(Glyph.Name name, int key)
        {
            poNodeCompare.name = name;
            poNodeCompare.SetKey(key);

            Glyph pData = (Glyph)baseFind(poNodeCompare);
            return pData;
        }

        private void privRemove(Glyph pImage)
        {
            Debug.Assert(pImage != null);
            baseRemove(pImage);
        }

        private void privDump()
        {
            baseDump();
        }

        private static GlyphMan privGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        protected override NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new Glyph();
            Debug.Assert(pNodeBase != null);
            return pNodeBase;
        }

        private readonly Glyph poNodeCompare;
        private static GlyphMan pInstance = null;
    }
}