using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialMedia.Core.CustomEntities
{
    public class PagedList<T> : List<T>
    {
        //Página actual
        public int CurrentPage { get; set; }
        //Total de páginas
        public int TotalPages { get; set; }
        //Cantidad de elementos por página
        public int PageSize { get; set; }
        //Total de elementos
        public int TotalCount { get; set; }
        //Bandera para saber si se puede ir a una página anterior
        public bool HasPreviousPage => CurrentPage > 1;
        //Bandera para saber si se puede ir a una página siguiente
        public bool HasNextPage => CurrentPage < TotalPages;
        //Número de la página siguiente
        public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : (int?)null;
        //Número de la página anterior
        public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : (int?)null;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            //Calcular total de páginas teniendo en cuenta que al obtener decimales se añadirá una página extra
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            //Agregar listado de elementos recibidos
            AddRange(items);
        }

        public static PagedList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            //Obtener cantidad de elementos
            var count = source.Count();
            //Se usa Skip para ignorar los primeros x cantidad de elementos
            //Se usa Take para tomar x cantidad de elementos
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            //Retornar listado de elementos ya paginados
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
