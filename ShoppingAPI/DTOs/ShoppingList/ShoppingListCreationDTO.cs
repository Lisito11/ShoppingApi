using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingAPI.DTOs.ShoppingList
{
	public class ShoppingListCreationDTO
	{
        public Guid? SuperMarketId { get; set; }

        public Guid? UserId { get; set; }

        public DateTime? created { get; set; }
    }
}

