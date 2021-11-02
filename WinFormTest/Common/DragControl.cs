using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest
{

    public class DragControl

    {

        //待拖动的控件

        private Control m_Control;

        //鼠标按下时的x，y坐标

        private int m_X;

        private int m_Y;

        public DragControl(Control control)
        {
            m_Control = control;

            m_Control.MouseDown += new MouseEventHandler(control_MouseDown);

            m_Control.MouseMove += new MouseEventHandler(contro_MouseMove);

        }

        private void control_MouseDown(object sender, MouseEventArgs e)
        {

            m_X = e.X;

            m_Y = e.Y;

        }
        private void contro_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                int x = e.X - m_X;

                int y = e.Y - m_Y;

                this.m_Control.Left += x;

                this.m_Control.Top += y;
            }
        }
    }

}
