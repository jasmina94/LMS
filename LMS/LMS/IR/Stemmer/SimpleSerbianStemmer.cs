using Lucene.Net.Tartarus.Snowball;

namespace LMS.IR.Stemmer
{
    public class SimpleSerbianStemmer : SnowballProgram
    {
        private readonly static SimpleSerbianStemmer methodObject = new SimpleSerbianStemmer();

        private readonly static Among [] a_0 = {
            new Among("daba", -1, 66, "", methodObject),
            new Among("ajaca", -1, 11, "", methodObject),
            new Among("ejaca", -1, 13, "", methodObject),
            new Among("ljaca", -1, 12, "", methodObject),
            new Among("njaca", -1, 78, "", methodObject),
            new Among("ojaca", -1, 14, "", methodObject),
            new Among("alaca", -1, 75, "", methodObject),
            new Among("elaca", -1, 76, "", methodObject),
            new Among("olaca", -1, 77, "", methodObject),
            new Among("maca", -1, 68, "", methodObject),
            new Among("naca", -1, 69, "", methodObject),
            new Among("raca", -1, 74, "", methodObject),
            new Among("saca", -1, 73, "", methodObject),
            new Among("vaca", -1, 72, "", methodObject),
            new Among("\u0161aca", -1, 17, "", methodObject),
            new Among("ajca", -1, 11, "", methodObject),
            new Among("ejca", -1, 13, "", methodObject),
            new Among("ljca", -1, 12, "", methodObject),
            new Among("ojca", -1, 14, "", methodObject),
            new Among("aoca", -1, 75, "", methodObject),
            new Among("\u0161ca", -1, 17, "", methodObject),
            new Among("loga", -1, 1, "", methodObject),
            new Among("acaka", -1, 51, "", methodObject),
            new Among("ajaka", -1, 15, "", methodObject),
            new Among("ojaka", -1, 16, "", methodObject),
            new Among("anaka", -1, 71, "", methodObject),
            new Among("ataka", -1, 53, "", methodObject),
            new Among("etaka", -1, 54, "", methodObject),
            new Among("itaka", -1, 55, "", methodObject),
            new Among("otaka", -1, 56, "", methodObject),
            new Among("utaka", -1, 57, "", methodObject),
            new Among("ojska", -1, 60, "", methodObject),
            new Among("esama", -1, 61, "", methodObject),
            new Among("izama", -1, 80, "", methodObject),
            new Among("cima", -1, 67, "", methodObject),
            new Among("jacima", 34, 5, "", methodObject),
            new Among("nicima", 34, 22, "", methodObject),
            new Among("ticima", 34, 23, "", methodObject),
            new Among("teticima", 37, 20, "", methodObject),
            new Among("zicima", 34, 24, "", methodObject),
            new Among("atcima", 34, 53, "", methodObject),
            new Among("utcima", 34, 57, "", methodObject),
            new Among("pesima", -1, 2, "", methodObject),
            new Among("inzima", -1, 18, "", methodObject),
            new Among("lozima", -1, 1, "", methodObject),
            new Among("metara", -1, 62, "", methodObject),
            new Among("centara", -1, 63, "", methodObject),
            new Among("istara", -1, 64, "", methodObject),
            new Among("ekata", -1, 79, "", methodObject),
            new Among("nstava", -1, 21, "", methodObject),
            new Among("kustava", -1, 28, "", methodObject),
            new Among("njac", -1, 78, "", methodObject),
            new Among("anjac", 51, 10, "", methodObject),
            new Among("alac", -1, 75, "", methodObject),
            new Among("elac", -1, 76, "", methodObject),
            new Among("olac", -1, 77, "", methodObject),
            new Among("mac", -1, 68, "", methodObject),
            new Among("nac", -1, 69, "", methodObject),
            new Among("rac", -1, 74, "", methodObject),
            new Among("sac", -1, 73, "", methodObject),
            new Among("vac", -1, 72, "", methodObject),
            new Among("jebe", -1, 81, "", methodObject),
            new Among("olce", -1, 77, "", methodObject),
            new Among("kuse", -1, 26, "", methodObject),
            new Among("rave", -1, 40, "", methodObject),
            new Among("\u0161ave", -1, 49, "", methodObject),
            new Among("ci", -1, 67, "", methodObject),
            new Among("baci", 66, 82, "", methodObject),
            new Among("jaci", 66, 5, "", methodObject),
            new Among("tvenici", 66, 19, "", methodObject),
            new Among("snici", 66, 25, "", methodObject),
            new Among("tetici", 66, 20, "", methodObject),
            new Among("bojci", 66, 4, "", methodObject),
            new Among("vojci", 66, 3, "", methodObject),
            new Among("atci", 66, 53, "", methodObject),
            new Among("itci", 66, 55, "", methodObject),
            new Among("utci", 66, 57, "", methodObject),
            new Among("cajni", -1, 6, "", methodObject),
            new Among("larni", -1, 8, "", methodObject),
            new Among("erni", -1, 7, "", methodObject),
            new Among("esni", -1, 9, "", methodObject),
            new Among("pesi", -1, 2, "", methodObject),
            new Among("inzi", -1, 18, "", methodObject),
            new Among("lozi", -1, 1, "", methodObject),
            new Among("acak", -1, 51, "", methodObject),
            new Among("atak", -1, 53, "", methodObject),
            new Among("etak", -1, 54, "", methodObject),
            new Among("itak", -1, 55, "", methodObject),
            new Among("otak", -1, 56, "", methodObject),
            new Among("utak", -1, 57, "", methodObject),
            new Among("u\u0161ak", -1, 52, "", methodObject),
            new Among("izam", -1, 80, "", methodObject),
            new Among("skom", -1, 84, "", methodObject),
            new Among("tican", -1, 59, "", methodObject),
            new Among("voljan", -1, 70, "", methodObject),
            new Among("eskan", -1, 58, "", methodObject),
            new Among("alan", -1, 38, "", methodObject),
            new Among("bilan", -1, 31, "", methodObject),
            new Among("gilan", -1, 35, "", methodObject),
            new Among("nilan", -1, 37, "", methodObject),
            new Among("rilan", -1, 36, "", methodObject),
            new Among("silan", -1, 34, "", methodObject),
            new Among("tilan", -1, 32, "", methodObject),
            new Among("avilan", -1, 33, "", methodObject),
            new Among("atan", -1, 45, "", methodObject),
            new Among("pletan", -1, 48, "", methodObject),
            new Among("tetan", -1, 47, "", methodObject),
            new Among("antan", -1, 30, "", methodObject),
            new Among("pravan", -1, 42, "", methodObject),
            new Among("stavan", -1, 41, "", methodObject),
            new Among("sivan", -1, 44, "", methodObject),
            new Among("tivan", -1, 43, "", methodObject),
            new Among("ozan", -1, 39, "", methodObject),
            new Among("a\u0161an", -1, 83, "", methodObject),
            new Among("du\u0161an", -1, 29, "", methodObject),
            new Among("kusin", -1, 27, "", methodObject),
            new Among("metar", -1, 62, "", methodObject),
            new Among("centar", -1, 63, "", methodObject),
            new Among("istar", -1, 64, "", methodObject),
            new Among("ekat", -1, 79, "", methodObject),
            new Among("anat", -1, 50, "", methodObject),
            new Among("enat", -1, 46, "", methodObject),
            new Among("o\u0161cu", -1, 65, "", methodObject),
            new Among("i\u0161tu", -1, 85, "", methodObject) };

        private readonly static char [] g_v = new[] { (char)17, (char)65, (char)16 };

        private readonly static char [] g_sa = new[] { (char)65, (char)4, (char)0, (char)0, (char)0, (char)0,
            (char)0, (char)0, (char)0, (char)0, (char)0, (char)4, (char)0, (char)0, (char)128 };

        private readonly static char [] g_ca = { (char)119, (char)95, (char)23, (char)1, (char)0, (char)0, (char)0,
            (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0,
            (char)0, (char)0, (char)32, (char)136, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, (char)0,
            (char)0, (char)0, (char)128, (char)0, (char)0, (char)0, (char)16 };

        private int I_p1;

        private void copy_from(SimpleSerbianStemmer other)
        {
            I_p1 = other.I_p1;
            copy_from(other);
        }

        private bool r_prelude()
        {
            int v_1;
            int v_2;
            int v_3;
            int v_4;
            int v_5;
            int v_6;
            // (, line 36
            // do, line 38
            v_1 = m_cursor;
            do
            {
                // repeat, line 38
                while (true)
                {
                    v_2 = m_cursor;
                    do
                    {
                        // goto, line 38
                        while (true)
                        {
                            v_3 = m_cursor;
                            do
                            {
                                // (, line 38
                                
                                if (!(InGrouping(g_ca, 98, 382)))
                                {
                                    break;
                                }
                                // [, line 39
                                m_bra = m_cursor;
                                // literal, line 39                               
                                if (!(Eq_S(3, "ije")))
                                {
                                    break;
                                }
                                // ], line 39
                                m_ket = m_cursor;
                                if (!(InGrouping(g_ca, 98, 382)))
                                {
                                    break;
                                }
                                // <-, line 39
                                SliceFrom("e");
                                m_cursor = v_3;
                                break;
                            } while (false);
                            m_cursor = v_3;
                            if (m_cursor >= m_limit)
                            {
                                break;
                            }
                            m_cursor++;
                        }
                        continue;
                    } while (false);
                    m_cursor = v_2;
                    break;
                }
            } while (false);
            m_cursor = v_1;
            // do, line 42
            v_4 = m_cursor;
            do
            {
                // repeat, line 42
                while (true)
                {
                    v_5 = m_cursor;
                    do
                    {
                        // goto, line 42
                        while (true)
                        {
                            v_6 = m_cursor;
                            do
                            {
                                // (, line 42
                                if (!(InGrouping(g_ca, 98, 382)))
                                {
                                    break;
                                }
                                // [, line 43
                                m_bra = m_cursor;
                                // literal, line 43
                                if (!(Eq_S(2, "je")))
                                {
                                    break;
                                }
                                // ], line 43
                                m_ket = m_cursor;
                                if (!(InGrouping(g_ca, 98, 382)))
                                {
                                    break;
                                }
                                // <-, line 43
                                SliceFrom("e");
                                m_cursor = v_6;
                                break;
                            } while (false);
                            m_cursor = v_6;
                            if (m_cursor >= m_limit)
                            {
                                break;
                            }
                            m_cursor++;
                        }
                        continue;
                    } while (false);
                    m_cursor = v_5;
                    break;
                }
            } while (false);
            m_cursor = v_4;
            return true;
        }

        private bool r_mark_regions()
        {
            // (, line 47
            I_p1 = m_limit;
            // gopast, line 51
            while (true)
            {
                do
                {
                    if (!(InGrouping(g_v, 97, 117)))
                    {
                        break;
                    }
                    break;
                } while (false);

                if (m_cursor >= m_limit)
                {
                    return false;
                }

                m_cursor++;
            }

            // gopast, line 51
            while (true)
            {
                do
                {
                    if (!(OutGrouping(g_v, 97, 117)))
                    {
                        break;
                    }
                    break;
                } while (false);
                if (m_cursor >= m_limit)
                {
                    return false;
                }
                m_cursor++;
            }

            // setmark p1, line 51
            I_p1 = m_cursor;
            return true;
        }

        private bool r_R1()
        {
            if (!(I_p1 <= m_cursor))
            {
                return false;
            }
            return true;
        }

        private bool r_Step_1()
        {
            int among_var;
            // (, line 59
            // [, line 60
            m_ket = m_cursor;
            // substring, line 60
            among_var = FindAmongB(a_0, 124);
            if (among_var == 0)
            {
                return false;
            }
            // ], line 60
            m_bra = m_cursor;
            // call R1, line 61
            if (!r_R1())
            {
                return false;
            }
            switch (among_var)
            {
                case 0:
                    return false;
                case 1:
                    // (, line 65
                    // <-, line 65
                    SliceFrom("log");
                    break;
                case 2:
                    // (, line 67
                    // <-, line 67
                    SliceFrom("peh");
                    break;
                case 3:
                    // (, line 68
                    // <-, line 68
                    SliceFrom("vojka");
                    break;
                case 4:
                    // (, line 69
                    // <-, line 69
                    SliceFrom("bojka");
                    break;
                case 5:
                    // (, line 71
                    // <-, line 71
                    SliceFrom("jak");
                    break;
                case 6:
                    // (, line 72
                    // <-, line 72
                    SliceFrom("cajan");
                    break;
                case 7:
                    // (, line 73
                    // <-, line 73
                    SliceFrom("eran");
                    break;
                case 8:
                    // (, line 74
                    // <-, line 74
                    SliceFrom("laran");
                    break;
                case 9:
                    // (, line 75
                    // <-, line 75
                    SliceFrom("esan");
                    break;
                case 10:
                    // (, line 76
                    // <-, line 76
                    SliceFrom("anjca");
                    break;
                case 11:
                    // (, line 78
                    // <-, line 78
                    SliceFrom("ajca");
                    break;
                case 12:
                    // (, line 80
                    // <-, line 80
                    SliceFrom("ljac");
                    break;
                case 13:
                    // (, line 82
                    // <-, line 82
                    SliceFrom("ejca");
                    break;
                case 14:
                    // (, line 84
                    // <-, line 84
                    SliceFrom("ojac");
                    break;
                case 15:
                    // (, line 85
                    // <-, line 85
                    SliceFrom("ajka");
                    break;
                case 16:
                    // (, line 86
                    // <-, line 86
                    SliceFrom("ojka");
                    break;
                case 17:
                    // (, line 88
                    // <-, line 88
                    SliceFrom("\u0161ac");
                    break;
                case 18:
                    // (, line 90
                    // <-, line 90
                    SliceFrom("ing");
                    break;
                case 19:
                    // (, line 91
                    // <-, line 91
                    SliceFrom("tvenik");
                    break;
                case 20:
                    // (, line 93
                    // <-, line 93
                    SliceFrom("tetika");
                    break;
                case 21:
                    // (, line 94
                    // <-, line 94
                    SliceFrom("nstvo");
                    break;
                case 22:
                    // (, line 95
                    // <-, line 95
                    SliceFrom("nik");
                    break;
                case 23:
                    // (, line 96
                    // <-, line 96
                    SliceFrom("tik");
                    break;
                case 24:
                    // (, line 97
                    // <-, line 97
                    SliceFrom("zik");
                    break;
                case 25:
                    // (, line 98
                    // <-, line 98
                    SliceFrom("snik");
                    break;
                case 26:
                    // (, line 99
                    // <-, line 99
                    SliceFrom("kusi");
                    break;
                case 27:
                    // (, line 100
                    // <-, line 100
                    SliceFrom("kusan");
                    break;
                case 28:
                    // (, line 101
                    // <-, line 101
                    SliceFrom("kustvo");
                    break;
                case 29:
                    // (, line 102
                    // <-, line 102
                    SliceFrom("du\u0161ni");
                    break;
                case 30:
                    // (, line 103
                    // <-, line 103
                    SliceFrom("antni");
                    break;
                case 31:
                    // (, line 104
                    // <-, line 104
                    SliceFrom("bilni");
                    break;
                case 32:
                    // (, line 105
                    // <-, line 105
                    SliceFrom("tilni");
                    break;
                case 33:
                    // (, line 106
                    // <-, line 106
                    SliceFrom("avilni");
                    break;
                case 34:
                    // (, line 107
                    // <-, line 107
                    SliceFrom("silni");
                    break;
                case 35:
                    // (, line 108
                    // <-, line 108
                    SliceFrom("gilni");
                    break;
                case 36:
                    // (, line 109
                    // <-, line 109
                    SliceFrom("rilni");
                    break;
                case 37:
                    // (, line 110
                    // <-, line 110
                    SliceFrom("nilni");
                    break;
                case 38:
                    // (, line 111
                    // <-, line 111
                    SliceFrom("alni");
                    break;
                case 39:
                    // (, line 112
                    // <-, line 112
                    SliceFrom("ozni");
                    break;
                case 40:
                    // (, line 113
                    // <-, line 113
                    SliceFrom("ravi");
                    break;
                case 41:
                    // (, line 114
                    // <-, line 114
                    SliceFrom("stavni");
                    break;
                case 42:
                    // (, line 115
                    // <-, line 115
                    SliceFrom("pravni");
                    break;
                case 43:
                    // (, line 116
                    // <-, line 116
                    SliceFrom("tivni");
                    break;
                case 44:
                    // (, line 117
                    // <-, line 117
                    SliceFrom("sivni");
                    break;
                case 45:
                    // (, line 118
                    // <-, line 118
                    SliceFrom("atni");
                    break;
                case 46:
                    // (, line 119
                    // <-, line 119
                    SliceFrom("enta");
                    break;
                case 47:
                    // (, line 120
                    // <-, line 120
                    SliceFrom("tetni");
                    break;
                case 48:
                    // (, line 121
                    // <-, line 121
                    SliceFrom("pletni");
                    break;
                case 49:
                    // (, line 122
                    // <-, line 122
                    SliceFrom("\u0161avi");
                    break;
                case 50:
                    // (, line 123
                    // <-, line 123
                    SliceFrom("anta");
                    break;
                case 51:
                    // (, line 125
                    // <-, line 125
                    SliceFrom("acka");
                    break;
                case 52:
                    // (, line 126
                    // <-, line 126
                    SliceFrom("u\u0161ka");
                    break;
                case 53:
                    // (, line 130
                    // <-, line 130
                    SliceFrom("atka");
                    break;
                case 54:
                    // (, line 132
                    // <-, line 132
                    SliceFrom("etka");
                    break;
                case 55:
                    // (, line 135
                    // <-, line 135
                    SliceFrom("itka");
                    break;
                case 56:
                    // (, line 137
                    // <-, line 137
                    SliceFrom("otka");
                    break;
                case 57:
                    // (, line 141
                    // <-, line 141
                    SliceFrom("utka");
                    break;
                case 58:
                    // (, line 142
                    // <-, line 142
                    SliceFrom("eskna");
                    break;
                case 59:
                    // (, line 143
                    // <-, line 143
                    SliceFrom("ticni");
                    break;
                case 60:
                    // (, line 144
                    // <-, line 144
                    SliceFrom("ojsci");
                    break;
                case 61:
                    // (, line 145
                    // <-, line 145
                    SliceFrom("esma");
                    break;
                case 62:
                    // (, line 147
                    // <-, line 147
                    SliceFrom("metra");
                    break;
                case 63:
                    // (, line 149
                    // <-, line 149
                    SliceFrom("centra");
                    break;
                case 64:
                    // (, line 151
                    // <-, line 151
                    SliceFrom("istra");
                    break;
                case 65:
                    // (, line 152
                    // <-, line 152
                    SliceFrom("o\u0161ti");
                    break;
                case 66:
                    // (, line 153
                    // <-, line 153
                    SliceFrom("dba");
                    break;
                case 67:
                    // (, line 155
                    // <-, line 155
                    SliceFrom("cka");
                    break;
                case 68:
                    // (, line 157
                    // <-, line 157
                    SliceFrom("mca");
                    break;
                case 69:
                    // (, line 159
                    // <-, line 159
                    SliceFrom("nca");
                    break;
                case 70:
                    // (, line 160
                    // <-, line 160
                    SliceFrom("voljni");
                    break;
                case 71:
                    // (, line 161
                    // <-, line 161
                    SliceFrom("anki");
                    break;
                case 72:
                    // (, line 163
                    // <-, line 163
                    SliceFrom("vca");
                    break;
                case 73:
                    // (, line 165
                    // <-, line 165
                    SliceFrom("sca");
                    break;
                case 74:
                    // (, line 167
                    // <-, line 167
                    SliceFrom("rca");
                    break;
                case 75:
                    // (, line 170
                    // <-, line 170
                    SliceFrom("alca");
                    break;
                case 76:
                    // (, line 172
                    // <-, line 172
                    SliceFrom("elca");
                    break;
                case 77:
                    // (, line 175
                    // <-, line 175
                    SliceFrom("olca");
                    break;
                case 78:
                    // (, line 177
                    // <-, line 177
                    SliceFrom("njca");
                    break;
                case 79:
                    // (, line 179
                    // <-, line 179
                    SliceFrom("ekta");
                    break;
                case 80:
                    // (, line 181
                    // <-, line 181
                    SliceFrom("izma");
                    break;
                case 81:
                    // (, line 182
                    // <-, line 182
                    SliceFrom("jebi");
                    break;
                case 82:
                    // (, line 183
                    // <-, line 183
                    SliceFrom("baci");
                    break;
                case 83:
                    // (, line 184
                    // <-, line 184
                    SliceFrom("a\u0161ni");
                    break;
                case 84:
                    // (, line 185
                    // <-, line 185
                    SliceFrom("sko");
                    break;
                case 85:
                    // (, line 186
                    // <-, line 186
                    SliceFrom("i\u0161te");
                    break;
            }
            return true;
        }

        public override bool Stem()
        {
            int v_1;
            int v_2;
            int v_3;
            // (, line 192
            // do, line 193
            v_1 = m_cursor;
            do
            {
                // call prelude, line 193
                if (!r_prelude())
                {
                    break;
                }
            } while (false);
            m_cursor = v_1;
            // do, line 194
            v_2 = m_cursor;
            do
            {
                // call mark_regions, line 194
                if (!r_mark_regions())
                {
                    break;
                }
            } while (false);
            m_cursor = v_2;
            // backwards, line 195
            m_limit_backward = m_cursor;
            m_cursor = m_limit;
            // (, line 195
            // do, line 196
            v_3 = m_limit - m_cursor;
            do
            {
                // call Step_1, line 196
                if (!r_Step_1())
                {
                    break;
                }
            } while (false);
            m_cursor = m_limit - v_3;
            m_cursor = m_limit_backward;
            return true;
        }
    }
}