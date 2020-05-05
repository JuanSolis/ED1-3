using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ecommerceED1_2.Models;

namespace ecommerceED1_2.Models
{
    public class ArbolAVL<T,U> where T:IComparable<T>
    {
        Nodo<T, U> root;
        U nodoFoundValue;
        public ArbolAVL()
        {
            root = null;

        }

        private int max(int l, int r)
        {
            return l > r ? l : r;
        }

        public void Add(Nodo<T, U> n)
        {

            if (root == null)
            {
                root = n;
            }
            else
                Insert(root, n);
        }

        public void Insert(Nodo<T, U> raiz, Nodo<T, U> n)
        {
            if (raiz == null)
            {
                raiz = n;
                raiz = balance_tree(raiz);
            }
            else
            {
                if (n.indice.CompareTo(raiz.indice) < 0)
                {
                    if (raiz.izquierdo == null)
                    {
                        raiz.izquierdo = n;
                        raiz.izquierdo = balance_tree(raiz.izquierdo);
                    }

                    else
                        Insert(raiz.izquierdo, n);
                }
                else
                {
                    if (raiz.derecho == null)
                    {
                        raiz.derecho = n;
                        raiz.derecho = balance_tree(raiz.derecho);
                    }
                    else
                    {
                        Insert(raiz.derecho, n);
                    }
                }
            }


        }
        private Nodo<T, U> balance_tree(Nodo<T, U> current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.izquierdo) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (balance_factor(current.derecho) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }
        private int getHeight(Nodo<T, U> current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.izquierdo);
                int r = getHeight(current.derecho);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }
        public void DisplayTree()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            InOrderDisplayTree(root);
            Console.WriteLine();
        }

        public U Search(T indice)
        {
            InOrderSearchTree(indice, root);
            return nodoFoundValue;
        }
        private void InOrderDisplayTree(Nodo<T, U> current)
        {
            if (current != null)
            {
                InOrderDisplayTree(current.izquierdo);
                Console.Write("({0}, {1}) ", current.indice, current.valor);
                InOrderDisplayTree(current.derecho);
            }
        }

        private void InOrderSearchTree(T itemSearching, Nodo<T, U> currentItem)
        {

            if (currentItem != null)
            {

                InOrderSearchTree(itemSearching, currentItem.izquierdo);
                //Console.WriteLine("({0} no es igual a  {1}) ", currentItem.indice, itemSearching);
                InOrderSearchTree(itemSearching, currentItem.derecho);

                if (currentItem.indice.CompareTo(itemSearching) == 0)
                {
                    nodoFoundValue = currentItem.valor;
                }

            }
            else
            {
                return;
            }
        }

        private void nodeFound(Nodo<T, U> found)
        {
            Console.WriteLine("({0} tiene como elemento {1}) ", found.indice, found.valor);
        }

        private int balance_factor(Nodo<T, U> current)
        {
            int l = getHeight(current.izquierdo);
            int r = getHeight(current.derecho);
            int b_factor = l - r;
            return b_factor;
        }
        private Nodo<T, U> RotateRR(Nodo<T, U> padre)
        {
            Nodo<T, U> pivote = padre.derecho;
            padre.derecho = pivote.izquierdo;
            pivote.izquierdo = padre;
            return pivote;
        }
        private Nodo<T, U> RotateLL(Nodo<T, U> Padre)
        {
            Nodo<T, U> pivote = Padre.izquierdo;
            Padre.izquierdo = pivote.derecho;
            pivote.derecho = Padre;
            return pivote;
        }
        private Nodo<T, U> RotateLR(Nodo<T, U> Padre)
        {
            Nodo<T, U> pivote = Padre.izquierdo;
            Padre.izquierdo = RotateRR(pivote);
            return RotateLL(Padre);
        }
        private Nodo<T, U> RotateRL(Nodo<T, U> Padre)
        {
            Nodo<T, U> pivot = Padre.derecho;
            Padre.derecho = RotateLL(pivot);
            return RotateRR(Padre);
        }

    

}
}