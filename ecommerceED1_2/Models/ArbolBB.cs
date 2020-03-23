using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecommerceED1_2.Models
{
    public class ArbolBB
    {
        //Creacion de Raiz de tipo nodo con tipos String e Int

        public Nodo<string, int> raiz;
        public Nodo<string, int> aux;
        public int i = 0;


        public ArbolBB()
        {
            raiz = null;
        }

        private static int Alturas(Nodo<string, int> nodo)
        {
            return nodo = null ? -1 : nodo.altura;
        }
        public Nodo<string, int> InsertarNodo(int lineaNodo, string indiceNodo, Nodo<string, int> nodo)
        {
            Nodo<string, int> temp = null;

            if (nodo == null)
            {
                temp = new Nodo<string, int>();
                temp.nomFarmaco = indiceNodo;
                temp.lineaArchivo = lineaNodo;

                return temp;
            }
            // Solo se toma si es mayor y menor ya que no hay elementos iguales.

            if (comparadorString(indiceNodo, nodo.nomFarmaco) < 0)
            {
                nodo.nodoIzq = InsertarNodo(lineaNodo, indiceNodo, nodo.nodoIzq);

            }

            else
            {
                nodo.nodoDer = InsertarNodo(lineaNodo, indiceNodo, nodo.nodoDer);
            }

            return nodo;
        }
        public void Eliminar(string indiceNodo, ref Nodo<string, int> nodo)
        {
            if (nodo != null)
            {
                if (comparadorString(indiceNodo, nodo.nomFarmaco) < 0) //si es menor
                {
                    Eliminar(indiceNodo, ref nodo.nodoIzq);

                }
                else
                {
                    if (comparadorString(indiceNodo, nodo.nomFarmaco) > 0) //si es mayor
                    {
                        Eliminar(indiceNodo, ref nodo.nodoDer);
                    }
                    else
                    {
                        Nodo<string, int> nodoaeliminar = nodo; //se ubicó el nodo a eliminar
                        if (nodoaeliminar.nodoDer == null)
                        {
                            nodo = nodoaeliminar.nodoIzq;
                        }
                        else
                        {
                            if (nodoaeliminar.nodoIzq == null)
                            {
                                nodo = nodoaeliminar.nodoDer;
                            }
                            else
                            {
                                if (Alturas(nodo.nodoIzq) - Alturas(nodo.nodoDer) > 0)
                                {
                                    Nodo<string, int> nodoaux = null;
                                    Nodo<string, int> auxiliar = nodo.nodoIzq;
                                    bool Bandera = false;

                                    while (auxiliar.nodoDer != null)
                                    {
                                        nodoaux = auxiliar;
                                        auxiliar = auxiliar.nodoDer;
                                        Bandera = true;
                                    }
                                    nodo.nomFarmaco = auxiliar.nomFarmaco;
                                    nodoaeliminar = auxiliar;

                                    if (Bandera == true)
                                    {
                                        nodoaux.nodoDer = auxiliar.nodoIzq;
                                    }
                                    else
                                    {
                                        nodo.nodoIzq = auxiliar.nodoIzq;
                                    }
                                }
                                else
                                {
                                    if (Alturas(nodo.nodoDer) - Alturas(nodo.nodoIzq) > 0)
                                    {
                                        Nodo<string, int> nodoaux = null;
                                        Nodo<string, int> auxiliar = nodo.nodoDer;
                                        bool Bandera = false;
                                        while (auxiliar.nodoIzq != null)
                                        {
                                            nodoaux = auxiliar;
                                            auxiliar = auxiliar.nodoIzq;
                                            Bandera = true;
                                        }
                                        nodo.nomFarmaco = auxiliar.nomFarmaco;
                                        nodoaeliminar = auxiliar;
                                        if (Bandera == true)
                                        {
                                            nodoaux.nodoIzq = auxiliar.nodoDer;
                                        }
                                        else
                                        {
                                            nodo.nodoDer = auxiliar.nodoDer;
                                        }
                                    }
                                    else
                                    {
                                        if (Alturas(nodo.nodoDer) - Alturas(nodo.nodoIzq) == 0)
                                        {
                                            Nodo<string, int> nodoaux = null;
                                            Nodo<string, int> auxiliar = nodo.nodoIzq;
                                            bool Bandera = false;
                                            while (auxiliar.nodoDer != null)
                                            {
                                                nodoaux = auxiliar;
                                                auxiliar = auxiliar.nodoDer;
                                                Bandera = true;
                                            }
                                            nodo.nomFarmaco = auxiliar.nomFarmaco;
                                            nodoaeliminar = auxiliar;
                                            if (Bandera == true)
                                            {
                                                nodoaux.nodoDer = auxiliar.nodoIzq;

                                            }
                                            else
                                            {
                                                nodo.nodoIzq = auxiliar.nodoIzq;
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        // Funcion para ver si funciona el arbol
        public void Tranversal(Nodo<string, int> nodo)
        {
            if (nodo == null)
                return;

            for (int n = 0; n < i; n++)
                Console.Write(" ");

            Console.WriteLine(nodo.nomFarmaco);

            if (nodo.nodoIzq != null)
            {
                i++;
                Console.Write("I ");
                Tranversal(nodo.nodoIzq);
                i--;
            }

            if (nodo.nodoDer != null)
            {
                i++;
                Console.Write("D ");
                Tranversal(nodo.nodoDer);
                i--;
            }
        }


        public delegate int delegadoComparador(string dato1, string raiz);

        delegadoComparador comparadorString = new delegadoComparador(ArbolBB.comparacionDeDatos);

        //Delegado para comparar los strings 
        public static int comparacionDeDatos(string datoEntrante, string Raiz)
        {
            return datoEntrante.CompareTo(Raiz);
        }

    }
}