﻿/*
 * Original source code link:
 * https://onedrive.live.com/?cid=9DA0FA00AC5A8258&id=9DA0FA00AC5A8258!337
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace pmd2mqo
{
    static class Pmd2mqo
    {
        static Encoding sjis = Encoding.GetEncoding("Shift_JIS");

        /*static void usage()
        {
            Console.WriteLine("Usage: pmd2mqo [/s scale] input.pmd ...");
            Console.WriteLine("  /s scale: Scaling (default = 10.0)");
            Console.WriteLine("注意: 出力後のmqoファイルは必ずMetasequoiaで読み込んで「近接する頂点をくっつける」などの加工を行うこと");
            Environment.Exit(-1);
        }

        static void Main(string[] args)
        {
            List<string> filename = new List<string>();
            float scale = 10;

            Regex ext = new Regex(@"\.pmd$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            string[] _args = Utilities.Argument.ExpandWildcards(args);
            bool getScale = false;
            foreach (string s in _args)
            {
                if (getScale)
                {
                    try
                    {
                        scale = float.Parse(s);
                    }
                    catch
                    {
                        Console.Error.WriteLine("警告: /s の後は数値である必要があります");
                    }
                    getScale = false;
                }
                else
                {
                    Match m = ext.Match(s);
                    if (m.Success)
                    {
                        filename.Add(s);
                    }
                    else if (s == "/s")
                    {
                        getScale = true;
                    }
                }
            }
            if (filename.Count == 0)
            {
                usage();
            }
            else
            {
                foreach (string s in filename) pmd2mqo_cui(s, scale);
                Console.WriteLine("注意: 出力後のmqoファイルは必ずMetasequoiaで読み込んで「近接する頂点をくっつける」などの加工を行うこと");
            }

        }*/

        static public bool pmd2mqo_cui(string pmdFile, float scale)
        {
        	Regex ext_pmd = new Regex(@"\.pmd$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            string mqoFile = ext_pmd.Replace(pmdFile, ".mqo");
            if (!ext_pmd.IsMatch(pmdFile)) mqoFile = pmdFile + ".mqo";
            try
            {
                pmd2mqo(pmdFile, mqoFile, scale);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }

        static void error_NoPMD()
        {
            throw new Exception("The file is not in PMD format!");
        }

        static public string readMultibyte(BinaryReader br, int num)
        {
            return sjis.GetString(br.ReadBytes(num).TakeWhile(c => c != 0).ToArray());
        }

        static public bool pmd2mqo(string pmdFile, string outFile, float scale = 1)
        {
            Regex ext_pmd = new Regex(@"\.pmd$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            string mqoFile = outFile; /*ext_pmd.Replace(pmdFile, ".mqo");
            if (!ext_pmd.IsMatch(pmdFile)) mqoFile = pmdFile + ".mqo"; //return false;//error_NoPMD();*/

            MqoDocument mqo = new MqoDocument();

            using (FileStream pmdFs = new FileStream(pmdFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (BinaryReader pmd = new BinaryReader(pmdFs))
                {
                    //Console.WriteLine("Parse start: " + pmdFile);

                    if (readMultibyte(pmd, 3) != "Pmd") return false;// error_NoPMD();
                    float pmdVer = pmd.ReadSingle();
                    string modelName = readMultibyte(pmd, 20);
                    string modelComment = readMultibyte(pmd, 256);

                    Console.WriteLine("Model Name: " + modelName);
                    Console.WriteLine("Comment: " + modelComment);

                    MqoObject mObj = mqo.mkObject(modelName);

                    // 頂点
                    uint count = pmd.ReadUInt32();
                    for (int i = 0; i < count; i++)
                    {
                        mObj.vertex.Add(new MqoVertex(pmd.ReadSingle() * scale, pmd.ReadSingle() * scale, pmd.ReadSingle() * scale));
                        pmd.ReadBytes(12); // 頂点法線を読み飛ばし
                        mObj.uv.Add(new MqoUV(pmd.ReadSingle(), pmd.ReadSingle()));
                        pmd.ReadBytes(2 + 2 + 1 + 1); // ボーン情報を読み飛ばし
                    }

                    // 面
                    count = pmd.ReadUInt32() / 3;
                    for (int i = 0; i < count; i++)
                    {
                        mObj.face.Add(new MqoFace3(pmd.ReadUInt16(), pmd.ReadUInt16(), pmd.ReadUInt16(), mObj.uv));
                    }

                    // 材質
                    count = pmd.ReadUInt32();
                    int foffset = 0;
                    for (int i = 0; i < count; i++)
                    {
                        MqoMaterial mat = new MqoMaterial(pmd.ReadSingle(), pmd.ReadSingle(), pmd.ReadSingle(), pmd.ReadSingle());
                        mat.matName = "mat" + i;
                        mqo.mat.Add(mat);
                        mat.power = pmd.ReadSingle();
                        mat.spc = (pmd.ReadSingle() + pmd.ReadSingle() + pmd.ReadSingle()) / 3;
                        mat.amb = (pmd.ReadSingle() + pmd.ReadSingle() + pmd.ReadSingle()) / 3;
                        pmd.ReadBytes(2); // toon, edge読み飛ばし

                        int fcount = (int)(pmd.ReadUInt32() / 3); // 適用面数
                        for (int j = 0; j < fcount; j++)
                        {
                            mObj.face[j + foffset].matId = i;
                        }
                        foffset += fcount;

                        mat.tex = readMultibyte(pmd, 20);
                    }

                    // ボーンは飛ばす
                    count = pmd.ReadUInt16();
                    pmd.ReadBytes((int)count * (20 + 2 + 2 + 1 + 2 + 12));

                    // IKは飛ばす
                    count = pmd.ReadUInt16();
                    for (int i = 0; i < count; i++)
                    {
                        pmd.ReadBytes(2 + 2);
                        int jcount = pmd.ReadByte();
                        pmd.ReadBytes(2 + 4 + 2 * jcount);
                    }

                    // Skin
                    count = pmd.ReadUInt16();
                    MqoObject sbase = null;
                    int[] sbaseIdx = null;
                    for (int i = 0; i < count; i++)
                    {
                        MqoObject so = mqo.mkObject(readMultibyte(pmd, 20));
                        uint vcount = pmd.ReadUInt32();
                        pmd.ReadByte();
                        if (i == 0)
                        {
                            sbase = so;
                            mObj.vertex.ForEach(v => sbase.vertex.Add(v));
                            sbaseIdx = new int[vcount];
                            for (int j = 0; j < vcount; j++)
                            {
                                int vId = (int)pmd.ReadUInt32();
                                MqoVertex v = new MqoVertex(
                                    pmd.ReadSingle() * scale,
                                    pmd.ReadSingle() * scale,
                                    pmd.ReadSingle() * scale);
                                sbaseIdx[j] = vId;
                                sbase.vertex[vId] = v;
                            }
                            mObj.face.FindAll(f => f.vId.Any(d => sbaseIdx.Contains(d))).ForEach(f => sbase.face.Add(f));
                        }
                        else
                        {
                            sbase.vertex.ForEach(v => so.vertex.Add(v));
                            int[] dvertex = new int[vcount];
                            for (int j = 0; j < vcount; j++)
                            {
                                int vId = sbaseIdx[pmd.ReadUInt32()];
                                dvertex[j] = vId;
                                so.vertex[vId] = new MqoVertex(
                                    sbase.vertex[vId].x + pmd.ReadSingle() * scale,
                                    sbase.vertex[vId].y + pmd.ReadSingle() * scale,
                                    sbase.vertex[vId].z + pmd.ReadSingle() * scale);
                            }
                            sbase.face.FindAll(f => f.vId.Any(d => dvertex.Contains(d))).ForEach(f => so.face.Add(f));
                        }
                    }

                    //Console.WriteLine("Parse end: " + pmdFile);
                }
            }

            using (TextWriter tw = new StreamWriter(mqoFile, false, sjis))
            {
                Console.WriteLine("Write to " + mqoFile);
                mqo.writeTo(tw);
                Console.WriteLine("end.");
            }
            return true;
        }
    }

    //-----------------------------------------------------
    // mqo出力に必要な分だけの実装
    // 出力したmqoは必ずMetasequoiaで開いて保存し直すこと

    class MqoMaterial
    {
        public float r, g, b, a;
        public float dif = 1.0f, amb = 0.6f, emi = 0.4f, spc = 0.0f, power = 5.0f;
        public string matName, tex;
        public MqoMaterial(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
            this.matName = "material";
            this.tex = "";
        }
        public override string ToString()
        {
            string str = String.Format("\"{0}\" col({1,0:F} {2,0:F} {3,0:F} {4,0:F}) dif({5,0:F}) amb({6,0:F}) emi({7,0:F}) spc({8,0:F}) power({9,0:F})",
                matName, r, g, b, a, dif, amb, emi, spc, power);
            if (tex != "")
            {
                str += " tex(\"" + tex + "\")";
            }
            return str;
        }
    }

    class MqoUV
    {
        public float u, v;
        public MqoUV(float u, float v)
        {
            this.u = u;
            this.v = v;
        }
        public override string ToString()
        {
            return String.Format("{0,0:F} {1,0:F}", u, v);
        }
    }

    class MqoVertex
    {
        public float x, y, z;
        public MqoVertex(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public override string ToString()
        {
            return String.Format("{0,0:F} {1,0:F} {2,0:F}", x, y, -z); // pmdとmqoはZ軸の向きが逆
        }
    }

    class MqoFace3
    {
        public int[] vId;
        public int matId;
        public MqoUV[] uv;
        public MqoFace3(int x, int y, int z, List<MqoUV> uv)
        {
            this.vId = new int[] { x, y, z };
            this.matId = 0;
            this.uv = new MqoUV[] { uv[x], uv[y], uv[z] };
        }
        public override string ToString()
        {
            return String.Format("3 V({0} {1} {2}) M({3}) UV({4} {5} {6})", vId[0], vId[1], vId[2], matId, uv[0], uv[1], uv[2]);
        }
    }

    class MqoObject
    {
        public string objName = "";
        public List<MqoVertex> vertex = new List<MqoVertex>();
        public List<MqoUV> uv = new List<MqoUV>();
        public List<MqoFace3> face = new List<MqoFace3>();
        public MqoObject(string oName)
        {
            objName = oName;
        }
        public void writeTo(TextWriter tw)
        {
            tw.WriteLine("Object \"" + objName + "\" {");

            tw.WriteLine("\tvertex " + vertex.Count + " {");
            vertex.ForEach(v => tw.WriteLine("\t\t" + v.ToString()));
            tw.WriteLine("\t}");

            tw.WriteLine("\tface " + face.Count + " {");
            face.ForEach(f => tw.WriteLine("\t\t" + f.ToString()));
            tw.WriteLine("\t}");

            tw.WriteLine("}");
        }
    }

    class MqoDocument
    {
        public List<MqoMaterial> mat = new List<MqoMaterial>();
        public List<MqoObject> obj = new List<MqoObject>();
        public void writeTo(TextWriter tw)
        {
            // ヘッダ
            tw.WriteLine("Metasequoia Document");
            tw.WriteLine("Format Text Ver 1.0");

            // 視点情報の書き出しなどは行わない

            // 材質の書き出し
            tw.WriteLine("Material " + mat.Count + " {");
            mat.ForEach(m => tw.WriteLine("\t" + m.ToString()));
            tw.WriteLine("}");

            // オブジェクトの書き出し
            obj.ForEach(o => o.writeTo(tw));

            // フッタ
            tw.WriteLine("Eof");
        }
        public MqoObject mkObject(string objName)
        {
            MqoObject mObj = new MqoObject(objName);
            obj.Add(mObj);
            return mObj;
        }
    }
}