using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.Models.ViewModels
{
	public class ProductVM
	{		
			public Product Product { get; set; }
			[ValidateNever]
			public IEnumerable<SelectListItem> CategoryList { get; set; }
	}
}

 // View models are designed ofr views
 // known as strongly typed views: there is a model sepecific to this view
 // 