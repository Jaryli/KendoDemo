﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KendoDemo.Models;
using KendoDemo.ViewModel;

namespace KendoDemo.Controllers
{
    public class MenuController : BaseController
    {
        private AppDbContext _db = new AppDbContext();

        //
        // GET: /Menu/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult XmlMenu()
        {
            var categories = _db.Categories.ToList();

            var products = _db.Products.ToList();

            var menus =
                categories.Select(
                    x =>
                    new MenuItemViewModel()
                        {
                            MenuId = x.CategoryId,
                            MenuName = x.Name,
                            MenuText = x.Name,
                            MenuImageUrl = "/Content/Images/menu.png",
                            MenuUrl = "/Category/Index",
                            MenuUrlTarget = "_black",
                            MenuSpriteCssClass = "menu",
                            MenuActionName = "/Category/Index",
                            SubMenuItemViewModels = products.Where(pro => pro.CategoryId == x.CategoryId).Select(p => new SubMenuItemViewModel
                                                                                                                 {
                                                                                                                     SubMenuId = p.ProductId,
                                                                                                                     SubMenuName = p.Name,
                                                                                                                     SubMenuText = p.Name,
                                                                                                                     SubMenuImageUrl = "/Content/Images/sub-menu.png",
                                                                                                                     SubMenuUrl = "/Product/Index",
                                                                                                                     SubMenuUrlTarget = "_black",
                                                                                                                     SubMenuSpriteCssClass = "sub-menu",
                                                                                                                     SubMenuActionName = "/Product/Index"
                                                                                                                 })
                        });

            return View(menus);
        }

        public ActionResult ObjectMenu()
        {
            var categories = _db.Categories.ToList();

            var products = _db.Products.ToList();

            var menus =
                categories.Select(
                    x =>
                    new MenuItemViewModel()
                    {
                        MenuId = x.CategoryId,
                        MenuName = x.Name,
                        MenuText = x.Name,
                        MenuImageUrl = "/Content/Images/menu.png",
                        MenuUrl = "/Category/Index",
                        MenuUrlTarget = "_black",
                        MenuSpriteCssClass = "menu",
                        MenuActionName = "/Category/Index",
                        SubMenuItemViewModels = products.Where(pro => pro.CategoryId == x.CategoryId).Select(p => new SubMenuItemViewModel
                        {
                            SubMenuId = p.ProductId,
                            SubMenuName = p.Name,
                            SubMenuText = p.Name,
                            SubMenuImageUrl = "/Content/Images/sub-menu.png",
                            SubMenuUrl = "/Product/Index",
                            SubMenuUrlTarget = "_black",
                            SubMenuSpriteCssClass = "sub-menu",
                            SubMenuActionName = "/Product/Index",
                        })
                    });

            return View(menus);
        }

        public ActionResult MainMenu()
        {
            var categories = _db.Categories.ToList();

            var products = _db.Products.ToList();

            var menus =
                categories.Select(
                    x =>
                    new MainMenuViewModel()
                    {
                        MenuId = x.CategoryId,
                        MenuName = x.Name,
                        MenuText = x.Name,
                        MenuImageUrl = "/Content/Images/menu.png",
                        MenuUrl = "/Category/Index",
                        MenuUrlTarget = "_black",
                        MenuSpriteCssClass = "menu",
                        MenuActionName = "/Category/Index",
                        SubMenuViewModels = products.Where(pro => pro.CategoryId == x.CategoryId).Select(p => new MainMenuViewModel
                        {
                            MenuId = p.ProductId,
                            MenuName = p.Name,
                            MenuText = p.Name,
                            MenuImageUrl = "/Content/Images/sub-menu.png",
                            MenuUrl = "/Product/Index",
                            MenuUrlTarget = "_black",
                            MenuSpriteCssClass = "sub-menu",
                            MenuActionName = "/Product/Index",
                            SubMenuViewModels = products.Where(pro => pro.CategoryId == x.CategoryId).Select(s => new MainMenuViewModel
                            {
                                MenuId = s.ProductId,
                                MenuName = s.Name,
                                MenuText = s.Name,
                                MenuImageUrl = "/Content/Images/sub-menu.png",
                                MenuUrl = "/Product/Index",
                                MenuUrlTarget = "_black",
                                MenuSpriteCssClass = "sub-menu",
                                MenuActionName = "/Product/Index",

                            })
                        })
                    });

            return View(menus);
        }

        public ActionResult Menu()
        {
            var menus = _db.Menus.ToList();

            var menuViewModelList = menus.Select(x => new MenuViewModel
                                                         {
                                                             MenuId = x.MenuId,
                                                             MenuName = x.MenuName,
                                                             MenuText = x.MenuText,
                                                             MenuImageUrl = x.MenuImageUrl,
                                                             MenuUrl = x.MenuUrl,
                                                             MenuUrlTarget = x.MenuUrlTarget,
                                                             MenuSpriteCssClass = x.MenuSpriteCssClass,
                                                             MenuSortOrder = x.MenuSortOrder,
                                                             ParentMenuId = x.ParentMenuId
                                                         }).ToList();

            List<MenuViewModel> viewModelList = new List<MenuViewModel>();

            //foreach (var mp in menus.Where(x => x.ParentMenuId == 0).ToList())
            //{
            //    var menuViewModel = new MenuViewModel()
            //                                      {
            //                                          MenuId = mp.MenuId,
            //                                          MenuName = mp.MenuName,
            //                                          MenuText = mp.MenuText,
            //                                          MenuImageUrl = mp.MenuImageUrl,
            //                                          MenuUrl = mp.MenuUrl,
            //                                          MenuUrlTarget = mp.MenuUrlTarget,
            //                                          MenuSpriteCssClass = mp.MenuSpriteCssClass,
            //                                          MenuSortOrder = mp.MenuSortOrder
            //                                      };

            //    if (menus.Count(p => p.ParentMenuId == mp.MenuId) > 0)
            //    {

            //    }

            //}

            foreach (var menuViewModel in menuViewModelList)
            {
                //if (menuViewModel.ParentMenuId > 0)
                //{
                //    var tempMenuViewModelList = new List<MenuViewModel>();

                //    //var tempMenuViewModel = menuViewModelList.SingleOrDefault(x => x.MenuId == menuViewModel.ParentMenuId);
                //    var tempMenuViewModel = viewModelList.SingleOrDefault(x => x.MenuId == menuViewModel.ParentMenuId);

                //    //tempMenuViewModelList.Add(tempMenuViewModel);
                //    tempMenuViewModelList.Add(menuViewModel);

                //    //menuViewModel.SubMenuViewModels = tempMenuViewModelList;
                //    if (tempMenuViewModel != null) tempMenuViewModel.SubMenuViewModels = tempMenuViewModelList;

                //}

                //if (menuViewModel.ParentMenuId == 0)
                //{
                //    viewModelList.Add(menuViewModel);
                //}

                var child = menuViewModelList.Where(x => x.ParentMenuId == menuViewModel.MenuId).ToList();
                menuViewModel.SubMenuViewModels.AddRange(child);
                viewModelList.Add(menuViewModel);

            }

            var finalList = viewModelList.Where(x => x.ParentMenuId == 0).ToList();

            //var menuViewModels = viewModelList;
            var menuViewModels = finalList;

            ViewBag.Menu = menuViewModels;

            //return View(menuViewModels);
            return View();
        }

        public ActionResult BaseMenu()
        {
            return View();
        }

        public ActionResult SelectMenu()
        {
            var categories = _db.Categories.ToList();

            var products = _db.Products.ToList();

            var menus =
                categories.Select(
                    x =>
                    new JsMenuViewModel()
                    {
                        Value = Convert.ToString(x.CategoryId),
                        Text = x.Name,
                        JsMenuItemViewModels = products.Where(pro => pro.CategoryId == x.CategoryId).Select(p => new JsMenuItemViewModel
                        {
                            Value = Convert.ToString(p.ProductId),
                            Text = p.Name
                        })
                    });

            return View(menus);
        }

        public JsonResult SelectMenuRead()
        {
            var categories = _db.Categories.ToList();

            var products = _db.Products.ToList();

            var models = categories.Select(
                    x =>
                    new dsMenuViewModel()
                    {
                        value = Convert.ToString(x.CategoryId),
                        text = x.Name,
                        items = products.Where(pro => pro.CategoryId == x.CategoryId).Select(p => new dsMenuViewModel
                        {
                            value = Convert.ToString(p.ProductId),
                            text = p.Name
                        })
                    });

            var menuSelect = new List<dsMenuViewModel>
                            {
                                new dsMenuViewModel { value = "0", text = "-- Select --", items = models}
                            };

            return Json(menuSelect, JsonRequestBehavior.AllowGet);
        }
    }
}
