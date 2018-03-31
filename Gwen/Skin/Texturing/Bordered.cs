using System;
using System.Drawing;

namespace Gwen.Skin.Texturing
{
    /// <summary>
    /// 3x3 texture grid.
    /// </summary>
    public struct Bordered
    {
        #region Constructors

        public Bordered(Texture texture, float x, float y, float w, float h, Margin inMargin, float drawMarginScale = 1.0f)
            : this()
        {
            m_Rects = new SubRect[9];
            for (int i = 0; i < m_Rects.Length; i++)
            {
                m_Rects[i].uv = new float[4];
            }

            Init(texture, x, y, w, h, inMargin, drawMarginScale);
        }

        #endregion Constructors

        #region Methods

        // can't have this as default param
        public void Draw(Renderer.RendererBase render, Rectangle r)
        {
            Draw(render, r, Color.White);
        }

        public void Draw(Renderer.RendererBase render, Rectangle r, Color col)
        {
            if (m_Texture == null)
                return;

            render.DrawColor = col;

            // this code would shrink window titles on small windows.
            // i have removed it, but i'm not positive of how it affects others

            // if (r.Width < m_Width && r.Height < m_Height)
            // {
            //     render.DrawTexturedRect(m_Texture, r, m_Rects[0].uv[0], m_Rects[0].uv[1], m_Rects[8].uv[2], m_Rects[8].uv[3]);
            //     return;
            // }
            var clip = render.ClipRegion;
            // clipping may not be enabled, but we're labeling that 'user fault' right now
            render.AddClipRegion(r);
            render.ClipRegion = new Rectangle(render.ClipRegion.X + r.X, render.ClipRegion.Y + r.Y, render.ClipRegion.Width, render.ClipRegion.Height);
            DrawRect(
                render,
                0,
                r.X,
                r.Y,
                m_Margin.Left,
                m_Margin.Top);
            DrawRect(
                render,
                1,
                r.X + m_Margin.Left,
                r.Y,
                r.Width - m_Margin.Left - m_Margin.Right,
                m_Margin.Top);
            DrawRect(
                render,
                 2,
                 (r.X + r.Width) - m_Margin.Right,
                 r.Y,
                 m_Margin.Right,
                 m_Margin.Top);

            DrawRect(
                render,
                3,
                r.X,
                r.Y + m_Margin.Top,
                m_Margin.Left,
                r.Height - m_Margin.Top - m_Margin.Bottom);
            DrawRect(
                render,
                4,
                r.X + m_Margin.Left,
                r.Y + m_Margin.Top,
                r.Width - m_Margin.Left - m_Margin.Right,
                r.Height - m_Margin.Top - m_Margin.Bottom);
            DrawRect(
                render,
                5,
                (r.X + r.Width) - m_Margin.Right,
                r.Y + m_Margin.Top,
                m_Margin.Right,
                r.Height - m_Margin.Top - m_Margin.Bottom);

            DrawRect(
                render,
                6,
                r.X,
                (r.Y + r.Height) - m_Margin.Bottom,
                m_Margin.Left,
                m_Margin.Bottom);
            DrawRect(
                render,
                7,
                r.X + m_Margin.Left,
                (r.Y + r.Height) - m_Margin.Bottom,
                r.Width - m_Margin.Left - m_Margin.Right,
                m_Margin.Bottom);
            DrawRect(
                render,
                8,
                (r.X + r.Width) - m_Margin.Right,
                (r.Y + r.Height) - m_Margin.Bottom,
                m_Margin.Right,
                m_Margin.Bottom);
                
            render.ClipRegion = clip;
        }

        #endregion Methods

        #region Fields

        private readonly SubRect[] m_Rects;
        private float m_Height;
        private Margin m_Margin;
        private Texture m_Texture;
        private float m_Width;

        #endregion Fields

        private void DrawRect(Renderer.RendererBase render, int i, int x, int y, int w, int h)
        {
            if (w > 0 && h > 0)
                render.DrawTexturedRect(m_Texture,
                                        new Rectangle(x, y, w, h),
                                        m_Rects[i].uv[0], m_Rects[i].uv[1], m_Rects[i].uv[2], m_Rects[i].uv[3]);
        }

        private void Init(Texture texture, float x, float y, float w, float h, Margin inMargin, float drawMarginScale = 1.0f)
        {
            m_Texture = texture;

            m_Margin = inMargin;

            SetRect(0,
            x,
            y,
            m_Margin.Left,
            m_Margin.Top);//TL

            SetRect(1,
            x + m_Margin.Left,
            y,
            w - m_Margin.Left - m_Margin.Right,
            m_Margin.Top);// Top middle

            SetRect(2,
            (x + w) - m_Margin.Right,
            y,
            m_Margin.Right,
            m_Margin.Top);//top right

            SetRect(3,
            x,
            y + m_Margin.Top,
            m_Margin.Left,
            h - m_Margin.Top - m_Margin.Bottom);// middle left

            SetRect(4,
            x + m_Margin.Left,
            y + m_Margin.Top,
            w - m_Margin.Left - m_Margin.Right,
            h - m_Margin.Top - m_Margin.Bottom);//middle middle

            SetRect(5,
            (x + w) - m_Margin.Right,
            y + m_Margin.Top,
            m_Margin.Right,
            h - m_Margin.Top - m_Margin.Bottom);//middle right

            SetRect(6,
            x,
            (y + h) - m_Margin.Bottom,
            m_Margin.Left,
            m_Margin.Bottom);//bottom left

            SetRect(7,
            x + m_Margin.Left,
            (y + h) - m_Margin.Bottom,
            w - m_Margin.Left - m_Margin.Right,
            m_Margin.Bottom);//bottom middle

            SetRect(8,
            (x + w) - m_Margin.Right,
            (y + h) - m_Margin.Bottom,
            m_Margin.Right,
            m_Margin.Bottom);//bottom right

            m_Margin.Left = (int)(m_Margin.Left * drawMarginScale);
            m_Margin.Right = (int)(m_Margin.Right * drawMarginScale);
            m_Margin.Top = (int)(m_Margin.Top * drawMarginScale);
            m_Margin.Bottom = (int)(m_Margin.Bottom * drawMarginScale);

            m_Width = w - x;
            m_Height = h - y;
        }

        private void SetRect(int num, float x, float y, float w, float h)
        {
            float texw = m_Texture.Width;
            float texh = m_Texture.Height;

            //x -= 1.0f;
            //y -= 1.0f;

            m_Rects[num].uv[0] = x / texw;
            m_Rects[num].uv[1] = y / texh;

            m_Rects[num].uv[2] = (x + w) / texw;
            m_Rects[num].uv[3] = (y + h) / texh;

            //	rects[num].uv[0] += 1.0f / m_Texture->width;
            //	rects[num].uv[1] += 1.0f / m_Texture->width;
        }
    }

    public struct SubRect
    {
        #region Fields

        public float[] uv;

        #endregion Fields
    }
}