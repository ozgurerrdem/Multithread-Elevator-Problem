using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yazlab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random rndm = new Random();
        int musteriSayisi = 0;
        private void btnStart_Click(object sender, EventArgs e)
        {
            Thread login = new Thread(loginThread);
            Thread exit = new Thread(exitThread);
            Thread elevator1 = new Thread(elevator_1);
            Thread elevator2 = new Thread(elevator_2);
            Thread elevator3 = new Thread(elevator_3);
            Thread elevator4 = new Thread(elevator_4);
            Thread elevator5 = new Thread(elevator_5);
            Thread control = new Thread(controlThread);
            login.Start();
            exit.Start();
            elevator1.Start();
            elevator2.Start();
            elevator3.Start();
            elevator4.Start();
            elevator5.Start();
            control.Start();
        }
        int sayacc = 0;
        int cikanSayisi = 0;
        int[] katMusteriSayisi = { 0, 0, 0, 0 };
        private void elevator_1()
        {
            int asansorKat = -1;
            String direction = "Up";
            int asansorKisiSayisi = 0;
            int[] gidilecekKatlar = { 0, 0, 0, 0 };
            int[] kattaInecek = { 0, 0, 0, 0 };
            while (true)
            {
                if (direction.Equals("Up"))//ASANSÖR YUKARI GİDİYORSA
                {
                    if (asansorKisiSayisi == 0)//ZEMİN KATTAYSA
                    {
                        Thread.Sleep(200);
                        label5.Text = "direction: up";
                        label3.Text = "floor: 0";
                        while (asansorKisiSayisi < 10)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (giren[i] > 0)
                                {
                                    if (asansorKisiSayisi < 10)
                                    {
                                        asansorKisiSayisi++;
                                        giren[i]--;
                                        kattaInecek[i]++;
                                        gidilecekKatlar[i] = 1;
                                    }
                                }
                            }
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            if (gidilecekKatlar[i] != 0)
                            {
                                label4.Text = "destination: " + (i + 1).ToString();
                            }
                        }
                    }
                    else//YUKARI HAREKET EDİYORSA
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Thread.Sleep(200);
                            if (gidilecekKatlar[i] != 0 && asansorKisiSayisi != 0)
                            {
                                asansorKat++;
                                label3.Text = "floor: " + (asansorKat + 1).ToString();
                                katMusteriSayisi[i] += kattaInecek[i];
                                if (i == 0)
                                {
                                    label9.Text = "1. floor: all: " + katMusteriSayisi[i].ToString();
                                }
                                if (i == 1)
                                {
                                    label10.Text = "2. floor: all: " + katMusteriSayisi[i].ToString();
                                }
                                if (i == 2)
                                {
                                    label11.Text = "3. floor: all: " + katMusteriSayisi[i].ToString();
                                }
                                if (i == 3)
                                {
                                    label12.Text = "4. floor: all: " + katMusteriSayisi[i].ToString();
                                }
                                asansorKisiSayisi -= kattaInecek[i];
                                label7.Text = "count_inside: " + asansorKisiSayisi.ToString();
                                if (asansorKisiSayisi == 0)
                                {
                                    direction = "Down";
                                    break;
                                }
                            }
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            kattaInecek[i] = 0;
                            gidilecekKatlar[i] = 0;
                        }
                    }
                }
                else if (direction.Equals("Down"))//ASANSÖR AŞAĞI GİDİYORSA
                {
                    for (int i = asansorKat; i >= 0; i--)
                    {
                        Thread.Sleep(200);
                        label5.Text = "direction: down";
                        if (cikan[i] != 0)
                        {
                            label4.Text = "destination: 0";
                            label3.Text = "floor: " + (asansorKat + 1).ToString();
                            if (asansorKat == 0)
                            {
                                cikanSayisi += asansorKisiSayisi;
                                label17.Text = "exit count: " + cikanSayisi.ToString();
                                musteriSayisi -= asansorKisiSayisi;
                                asansorKisiSayisi = 0;
                                label7.Text = "count_inside: " + asansorKisiSayisi.ToString();
                                direction = "Up";
                                label3.Text = "floor: 0";
                                asansorKat = -1;
                            }
                            else
                            {
                                asansorKat--;
                                label3.Text = "floor: " + (asansorKat + 1).ToString();
                                if (asansorKisiSayisi + cikan[i] > 10)
                                {
                                    if (katCikisIstegiSayilari[i] - 10 - asansorKisiSayisi < 0)
                                    {
                                        katCikisIstegiSayilari[i] -= 0;
                                    }
                                    else
                                    {
                                        katCikisIstegiSayilari[i] -= 10 - asansorKisiSayisi;
                                    }
                                    cikan[i] -= 10 - asansorKisiSayisi;
                                    asansorKisiSayisi = 10;
                                }
                                else
                                {
                                    if (katCikisIstegiSayilari[i] - cikan[i] < 0)
                                    {
                                        katCikisIstegiSayilari[i] = 0;
                                    }
                                    else
                                    {
                                        katCikisIstegiSayilari[i] -= cikan[i];
                                    }
                                    asansorKisiSayisi += cikan[i];
                                }
                                label7.Text = "count_inside: " + asansorKisiSayisi.ToString();
                            }
                        }
                    }
                }
            }
        }
        private void elevator_2()
        {
            int asansorKat = -1;
            String direction = "Up";
            int asansorKisiSayisi = 0;
            int[] gidilecekKatlar = { 0, 0, 0, 0 };
            int[] kattaInecek = { 0, 0, 0, 0 };

            while (true)
            {
                if (kontrol1 == true)
                {
                    if (direction.Equals("Up"))//ASANSÖR YUKARI GİDİYORSA
                    {
                        if (asansorKisiSayisi == 0)//ZEMİN KATTAYSA
                        {
                            Thread.Sleep(200);
                            label20.Text = "direction: up";
                            label22.Text = "floor: 0";
                            while (asansorKisiSayisi < 10)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (giren[i] > 0)
                                    {
                                        if (asansorKisiSayisi < 10)
                                        {
                                            asansorKisiSayisi++;
                                            giren[i]--;
                                            kattaInecek[i]++;
                                            gidilecekKatlar[i] = 1;
                                        }
                                    }
                                }
                            }
                            for (int i = 0; i < 4; i++)
                            {
                                if (gidilecekKatlar[i] != 0)
                                {
                                    label21.Text = "destination: " + (i + 1).ToString();
                                }
                            }
                        }
                        else//YUKARI HAREKET EDİYORSA
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Thread.Sleep(200);
                                if (gidilecekKatlar[i] != 0 && asansorKisiSayisi != 0)
                                {
                                    asansorKat++;
                                    label22.Text = "floor: " + (asansorKat + 1).ToString();
                                    katMusteriSayisi[i] += kattaInecek[i];
                                    if (i == 0)
                                    {
                                        label9.Text = "1. floor: all: " + katMusteriSayisi[i].ToString();
                                    }
                                    if (i == 1)
                                    {
                                        label10.Text = "2. floor: all: " + katMusteriSayisi[i].ToString();
                                    }
                                    if (i == 2)
                                    {
                                        label11.Text = "3. floor: all: " + katMusteriSayisi[i].ToString();
                                    }
                                    if (i == 3)
                                    {
                                        label12.Text = "4. floor: all: " + katMusteriSayisi[i].ToString();
                                    }
                                    asansorKisiSayisi -= kattaInecek[i];
                                    label18.Text = "count_inside: " + asansorKisiSayisi.ToString();
                                    if (asansorKisiSayisi == 0)
                                    {
                                        direction = "Down";
                                        break;
                                    }
                                }
                            }
                            for (int i = 0; i < 4; i++)
                            {
                                kattaInecek[i] = 0;
                                gidilecekKatlar[i] = 0;
                            }
                        }
                    }
                    else if (direction.Equals("Down"))//ASANSÖR AŞAĞI GİDİYORSA
                    {
                        for (int i = asansorKat; i >= 0; i--)
                        {
                            Thread.Sleep(200);
                            label20.Text = "direction: down";
                            if (cikan[i] != 0)
                            {
                                label21.Text = "destination: 0";
                                label22.Text = "floor: " + (asansorKat + 1).ToString();
                                if (asansorKat == 0)
                                {
                                    cikanSayisi += asansorKisiSayisi;
                                    label17.Text = "exit count: " + cikanSayisi.ToString();
                                    musteriSayisi -= asansorKisiSayisi;
                                    asansorKisiSayisi = 0;
                                    label18.Text = "count_inside: " + asansorKisiSayisi.ToString();
                                    direction = "Up";
                                    label22.Text = "floor: 0";
                                    asansorKat = -1;
                                }
                                else
                                {
                                    asansorKat--;
                                    label22.Text = "floor: " + (asansorKat + 1).ToString();
                                    if (asansorKisiSayisi + cikan[i] > 10)
                                    {
                                        if (katCikisIstegiSayilari[i] - 10 - asansorKisiSayisi < 0)
                                        {
                                            katCikisIstegiSayilari[i] -= 0;
                                        }
                                        else
                                        {
                                            katCikisIstegiSayilari[i] -= 10 - asansorKisiSayisi;
                                        }
                                        cikan[i] -= 10 - asansorKisiSayisi;
                                        asansorKisiSayisi = 10;
                                    }
                                    else
                                    {
                                        if (katCikisIstegiSayilari[i] - cikan[i] < 0)
                                        {
                                            katCikisIstegiSayilari[i] = 0;
                                        }
                                        else
                                        {
                                            katCikisIstegiSayilari[i] -= cikan[i];
                                        }
                                        asansorKisiSayisi += cikan[i];
                                    }
                                    label18.Text = "count_inside: " + asansorKisiSayisi.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }
        private void elevator_3()
        {
            int asansorKat = -1;
            String direction = "Up";
            int asansorKisiSayisi = 0;
            int[] gidilecekKatlar = { 0, 0, 0, 0 };
            int[] kattaInecek = { 0, 0, 0, 0 };
            while (true)
            {
                if (kontrol2 == true)
                {
                    if (direction.Equals("Up"))//ASANSÖR YUKARI GİDİYORSA
                    {
                        if (asansorKisiSayisi == 0)//ZEMİN KATTAYSA
                        {
                            Thread.Sleep(200);
                            label27.Text = "direction: up";
                            label29.Text = "floor: 0";
                            while (asansorKisiSayisi < 10)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (giren[i] > 0)
                                    {
                                        if (asansorKisiSayisi < 10)
                                        {
                                            asansorKisiSayisi++;
                                            giren[i]--;
                                            kattaInecek[i]++;
                                            gidilecekKatlar[i] = 1;
                                        }
                                    }
                                }
                            }
                            for (int i = 0; i < 4; i++)
                            {
                                if (gidilecekKatlar[i] != 0)
                                {
                                    label28.Text = "destination: " + (i + 1).ToString();
                                }
                            }
                        }
                        else//YUKARI HAREKET EDİYORSA
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Thread.Sleep(200);
                                if (gidilecekKatlar[i] != 0 && asansorKisiSayisi != 0)
                                {
                                    asansorKat++;
                                    label29.Text = "floor: " + (asansorKat + 1).ToString();
                                    katMusteriSayisi[i] += kattaInecek[i];
                                    if (i == 0)
                                    {
                                        label9.Text = "1. floor: all: " + katMusteriSayisi[i].ToString();
                                    }
                                    if (i == 1)
                                    {
                                        label10.Text = "2. floor: all: " + katMusteriSayisi[i].ToString();
                                    }
                                    if (i == 2)
                                    {
                                        label11.Text = "3. floor: all: " + katMusteriSayisi[i].ToString();
                                    }
                                    if (i == 3)
                                    {
                                        label12.Text = "4. floor: all: " + katMusteriSayisi[i].ToString();
                                    }
                                    asansorKisiSayisi -= kattaInecek[i];
                                    label25.Text = "count_inside: " + asansorKisiSayisi.ToString();
                                    if (asansorKisiSayisi == 0)
                                    {
                                        direction = "Down";
                                        break;
                                    }
                                }
                            }
                            for (int i = 0; i < 4; i++)
                            {
                                kattaInecek[i] = 0;
                                gidilecekKatlar[i] = 0;
                            }
                        }
                    }
                    else if (direction.Equals("Down"))//ASANSÖR AŞAĞI GİDİYORSA
                    {
                        for (int i = asansorKat; i >= 0; i--)
                        {
                            Thread.Sleep(200);
                            label27.Text = "direction: down";
                            if (cikan[i] != 0)
                            {
                                label28.Text = "destination: 0";
                                label29.Text = "floor: " + (asansorKat + 1).ToString();
                                if (asansorKat == 0)
                                {
                                    cikanSayisi += asansorKisiSayisi;
                                    label17.Text = "exit count: " + cikanSayisi.ToString();
                                    musteriSayisi -= asansorKisiSayisi;
                                    asansorKisiSayisi = 0;
                                    label25.Text = "count_inside: " + asansorKisiSayisi.ToString();
                                    direction = "Up";
                                    label29.Text = "floor: 0";
                                    asansorKat = -1;
                                }
                                else
                                {
                                    asansorKat--;
                                    label29.Text = "floor: " + (asansorKat + 1).ToString();
                                    if (asansorKisiSayisi + cikan[i] > 10)
                                    {
                                        if (katCikisIstegiSayilari[i] - 10 - asansorKisiSayisi < 0)
                                        {
                                            katCikisIstegiSayilari[i] -= 0;
                                        }
                                        else
                                        {
                                            katCikisIstegiSayilari[i] -= 10 - asansorKisiSayisi;
                                        }
                                        cikan[i] -= 10 - asansorKisiSayisi;
                                        asansorKisiSayisi = 10;
                                    }
                                    else
                                    {
                                        if (katCikisIstegiSayilari[i] - cikan[i] < 0)
                                        {
                                            katCikisIstegiSayilari[i] = 0;
                                        }
                                        else
                                        {
                                            katCikisIstegiSayilari[i] -= cikan[i];
                                        }
                                        asansorKisiSayisi += cikan[i];
                                    }
                                    label25.Text = "count_inside: " + asansorKisiSayisi.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }
        private void elevator_4()
        {
            int asansorKat = -1;
            String direction = "Up";
            int asansorKisiSayisi = 0;
            int[] gidilecekKatlar = { 0, 0, 0, 0 };
            int[] kattaInecek = { 0, 0, 0, 0 };
            while (true)
            {
                if (kontrol3 == true)
                {
                    if (direction.Equals("Up"))//ASANSÖR YUKARI GİDİYORSA
                    {
                        if (asansorKisiSayisi == 0)//ZEMİN KATTAYSA
                        {
                            Thread.Sleep(200);
                            label34.Text = "direction: up";
                            label36.Text = "floor: 0";
                            while (asansorKisiSayisi < 10)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (giren[i] > 0)
                                    {
                                        if (asansorKisiSayisi < 10)
                                        {
                                            asansorKisiSayisi++;
                                            giren[i]--;
                                            kattaInecek[i]++;
                                            gidilecekKatlar[i] = 1;
                                        }
                                    }
                                }
                            }
                            for (int i = 0; i < 4; i++)
                            {
                                if (gidilecekKatlar[i] != 0)
                                {
                                    label35.Text = "destination: " + (i + 1).ToString();
                                }
                            }
                        }
                        else//YUKARI HAREKET EDİYORSA
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Thread.Sleep(200);
                                if (gidilecekKatlar[i] != 0 && asansorKisiSayisi != 0)
                                {
                                    asansorKat++;
                                    label36.Text = "floor: " + (asansorKat + 1).ToString();
                                    katMusteriSayisi[i] += kattaInecek[i];
                                    if (i == 0)
                                    {
                                        label9.Text = "1. floor: all: " + katMusteriSayisi[i].ToString();
                                    }
                                    if (i == 1)
                                    {
                                        label10.Text = "2. floor: all: " + katMusteriSayisi[i].ToString();
                                    }
                                    if (i == 2)
                                    {
                                        label11.Text = "3. floor: all: " + katMusteriSayisi[i].ToString();
                                    }
                                    if (i == 3)
                                    {
                                        label12.Text = "4. floor: all: " + katMusteriSayisi[i].ToString();
                                    }
                                    asansorKisiSayisi -= kattaInecek[i];
                                    label32.Text = "count_inside: " + asansorKisiSayisi.ToString();
                                    if (asansorKisiSayisi == 0)
                                    {
                                        direction = "Down";
                                        break;
                                    }
                                }
                            }
                            for (int i = 0; i < 4; i++)
                            {
                                kattaInecek[i] = 0;
                                gidilecekKatlar[i] = 0;
                            }
                        }
                    }
                    else if (direction.Equals("Down"))//ASANSÖR AŞAĞI GİDİYORSA
                    {
                        for (int i = asansorKat; i >= 0; i--)
                        {
                            Thread.Sleep(200);
                            label34.Text = "direction: down";
                            if (cikan[i] != 0)
                            {
                                label35.Text = "destination: 0";
                                label36.Text = "floor: " + (asansorKat + 1).ToString();
                                if (asansorKat == 0)
                                {
                                    cikanSayisi += asansorKisiSayisi;
                                    label17.Text = "exit count: " + cikanSayisi.ToString();
                                    musteriSayisi -= asansorKisiSayisi;
                                    asansorKisiSayisi = 0;
                                    label32.Text = "count_inside: " + asansorKisiSayisi.ToString();
                                    direction = "Up";
                                    label36.Text = "floor: 0";
                                    asansorKat = -1;
                                }
                                else
                                {
                                    asansorKat--;
                                    label36.Text = "floor: " + (asansorKat + 1).ToString();
                                    if (asansorKisiSayisi + cikan[i] > 10)
                                    {
                                        if (katCikisIstegiSayilari[i] - 10 - asansorKisiSayisi < 0)
                                        {
                                            katCikisIstegiSayilari[i] -= 0;
                                        }
                                        else
                                        {
                                            katCikisIstegiSayilari[i] -= 10 - asansorKisiSayisi;
                                        }
                                        cikan[i] -= 10 - asansorKisiSayisi;
                                        asansorKisiSayisi = 10;
                                    }
                                    else
                                    {
                                        if (katCikisIstegiSayilari[i] - cikan[i] < 0)
                                        {
                                            katCikisIstegiSayilari[i] = 0;
                                        }
                                        else
                                        {
                                            katCikisIstegiSayilari[i] -= cikan[i];
                                        }
                                        asansorKisiSayisi += cikan[i];
                                    }
                                    label32.Text = "count_inside: " + asansorKisiSayisi.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }
        private void elevator_5()
        {
            int asansorKat = -1;
            String direction = "Up";
            int asansorKisiSayisi = 0;
            int[] gidilecekKatlar = { 0, 0, 0, 0 };
            int[] kattaInecek = { 0, 0, 0, 0 };
            while (true)
            {
                if (kontrol4 == true)
                {
                    if (direction.Equals("Up"))//ASANSÖR YUKARI GİDİYORSA
                    {
                        if (asansorKisiSayisi == 0)//ZEMİN KATTAYSA
                        {
                            Thread.Sleep(200);
                            label41.Text = "direction: up";
                            label43.Text = "floor: 0";
                            while (asansorKisiSayisi < 10)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (giren[i] > 0)
                                    {
                                        if (asansorKisiSayisi < 10)
                                        {
                                            asansorKisiSayisi++;
                                            giren[i]--;
                                            kattaInecek[i]++;
                                            gidilecekKatlar[i] = 1;
                                        }
                                    }
                                }
                            }
                            for (int i = 0; i < 4; i++)
                            {
                                if (gidilecekKatlar[i] != 0)
                                {
                                    label42.Text = "destination: " + (i + 1).ToString();
                                }
                            }
                        }
                        else//YUKARI HAREKET EDİYORSA
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Thread.Sleep(200);
                                if (gidilecekKatlar[i] != 0 && asansorKisiSayisi != 0)
                                {
                                    asansorKat++;
                                    label43.Text = "floor: " + (asansorKat + 1).ToString();
                                    katMusteriSayisi[i] += kattaInecek[i];
                                    if (i == 0)
                                    {
                                        label9.Text = "1. floor: all: " + katMusteriSayisi[i].ToString();
                                    }
                                    if (i == 1)
                                    {
                                        label10.Text = "2. floor: all: " + katMusteriSayisi[i].ToString();
                                    }
                                    if (i == 2)
                                    {
                                        label11.Text = "3. floor: all: " + katMusteriSayisi[i].ToString();
                                    }
                                    if (i == 3)
                                    {
                                        label12.Text = "4. floor: all: " + katMusteriSayisi[i].ToString();
                                    }
                                    asansorKisiSayisi -= kattaInecek[i];
                                    label39.Text = "count_inside: " + asansorKisiSayisi.ToString();
                                    if (asansorKisiSayisi == 0)
                                    {
                                        direction = "Down";
                                        break;
                                    }
                                }
                            }
                            for (int i = 0; i < 4; i++)
                            {
                                kattaInecek[i] = 0;
                                gidilecekKatlar[i] = 0;
                            }
                        }
                    }
                    else if (direction.Equals("Down"))//ASANSÖR AŞAĞI GİDİYORSA
                    {
                        for (int i = asansorKat; i >= 0; i--)
                        {
                            Thread.Sleep(200);
                            label41.Text = "direction: down";
                            if (cikan[i] != 0)
                            {
                                label42.Text = "destination: 0";
                                label43.Text = "floor: " + (asansorKat + 1).ToString();
                                if (asansorKat == 0)
                                {
                                    cikanSayisi += asansorKisiSayisi;
                                    label17.Text = "exit count: " + cikanSayisi.ToString();
                                    musteriSayisi -= asansorKisiSayisi;
                                    asansorKisiSayisi = 0;
                                    label39.Text = "count_inside: " + asansorKisiSayisi.ToString();
                                    direction = "Up";
                                    label43.Text = "floor: 0";
                                    asansorKat = -1;
                                }
                                else
                                {
                                    asansorKat--;
                                    label43.Text = "floor: " + (asansorKat + 1).ToString();
                                    if (asansorKisiSayisi + cikan[i] > 10)
                                    {
                                        if (katCikisIstegiSayilari[i] - 10 - asansorKisiSayisi < 0)
                                        {
                                            katCikisIstegiSayilari[i] -= 0;
                                        }
                                        else
                                        {
                                            katCikisIstegiSayilari[i] -= 10 - asansorKisiSayisi;
                                        }
                                        cikan[i] -= 10 - asansorKisiSayisi;
                                        asansorKisiSayisi = 10;
                                    }
                                    else
                                    {
                                        if (katCikisIstegiSayilari[i] - cikan[i] < 0)
                                        {
                                            katCikisIstegiSayilari[i] = 0;
                                        }
                                        else
                                        {
                                            katCikisIstegiSayilari[i] -= cikan[i];
                                        }
                                        asansorKisiSayisi += cikan[i];
                                    }
                                    label39.Text = "count_inside: " + asansorKisiSayisi.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        bool kontrol1 = false;
        bool kontrol2 = false;
        bool kontrol3 = false;
        bool kontrol4 = false;

        private void controlThread()
        {
            while (true)
            {
                if (musteriSayisi > 20 && kontrol1 == false)
                {
                    kontrol1 = true;
                    label24.Text = "active: true";
                    label23.Text = "mode: working";
                }
                if (musteriSayisi > 40 && kontrol2 == false)
                {
                    kontrol2 = true;
                    label31.Text = "active: true";
                    label30.Text = "mode: working";
                }
                if (musteriSayisi > 60 && kontrol3 == false)
                {
                    kontrol3 = true;
                    label38.Text = "active: true";
                    label37.Text = "mode: working";
                }
                if (musteriSayisi > 80 && kontrol4 == false)
                {
                    kontrol4 = true;
                    label45.Text = "active: true";
                    label44.Text = "mode: working";
                }
                if (musteriSayisi <= 20 && kontrol1 == true)
                {
                    kontrol1 = false;
                    label24.Text = "active: false";
                    label23.Text = "mode: idle";
                }
                if (musteriSayisi <= 40 && kontrol2 == true)
                {
                    kontrol2 = false;
                    label31.Text = "active: false";
                    label30.Text = "mode: idle";
                }
                if (musteriSayisi <= 60 && kontrol3 == true)
                {
                    kontrol3 = false;
                    label38.Text = "active: false";
                    label37.Text = "mode: idle";
                }
                if (musteriSayisi <= 80 && kontrol4 == true)
                {
                    kontrol4 = false;
                    label45.Text = "active: false";
                    label44.Text = "mode: idle";
                }
                Thread.Sleep(1000);
            }
        }

        int[] cikan = { 0, 0, 0, 0 };
        int[] katCikisIstegiSayilari = { 0, 0, 0, 0 };
        private void exitThread()
        {
            int kat, musteri;
            while (true)
            {
                Thread.Sleep(1000);
                kat = rndm.Next(4) + 1;
                musteri = rndm.Next(5) + 1;
                for (int i = 0; i < 4; i++)
                {
                    if (i + 1 == kat)
                    {
                        cikan[i] += musteri;
                        katCikisIstegiSayilari[i] += musteri;
                    }
                    if (i == 0)
                    {
                        label13.Text = "queue: " + katCikisIstegiSayilari[i].ToString();
                    }
                    else if (i == 1)
                    {
                        label14.Text = "queue: " + katCikisIstegiSayilari[i].ToString();
                    }
                    else if (i == 2)
                    {
                        label15.Text = "queue: " + katCikisIstegiSayilari[i].ToString();
                    }
                    else if (i == 3)
                    {
                        label16.Text = "queue: " + katCikisIstegiSayilari[i].ToString();
                    }
                }
            }
        }

        int[] giren = { 0, 0, 0, 0 };
        private void loginThread()
        {
            int kat, musteri;
            while (true)
            {
                Thread.Sleep(1000);
                kat = rndm.Next(4);
                musteri = rndm.Next(10) + 1;
                musteriSayisi += musteri;
                label8.Text = "0. floor: queue: " + musteriSayisi.ToString();
                for (int i = 0; i < 4; i++)
                {
                    if (i == kat)
                    {
                        giren[i] += musteri;
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }
    }
}
