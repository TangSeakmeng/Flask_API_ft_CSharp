from header import *
from classes.User import *
from classes.Menu import *
from classes.MenuCategory import *

app.add_url_rule("/api/user/create", view_func=User.index)
app.add_url_rule("/api/users/", view_func=User.getUser)
app.add_url_rule("/api/users/", view_func=User.searchUserByPublicId, methods=['post'])
app.add_url_rule("/api/user/", view_func=User.createUser, methods=['post'])
app.add_url_rule("/api/user/<publicId>", view_func=User.updateUser, methods=['put'])
app.add_url_rule("/api/user/<publicId>", view_func=User.deleteUserByPublicId, methods=['delete'])
app.add_url_rule("/login", view_func=User.Login, methods=['POST'])

app.add_url_rule("/api/menucategory/createtable/", view_func=MenuCategory.createTableMenuCategory)
app.add_url_rule("/api/menucategories", view_func=MenuCategory.getMenuCategories)
app.add_url_rule("/api/menucategory/<id>", view_func=MenuCategory.searchMenuCategoryById)
app.add_url_rule("/api/menucategory", view_func=MenuCategory.createMenuCategory, methods=['post'])
app.add_url_rule("/api/menucategory/getId", view_func=MenuCategory.getCategoryIdFromCategoryName, methods=['post'])
app.add_url_rule("/api/menucategory/<id>", view_func=MenuCategory.updateMenuCategory, methods=['put'])
app.add_url_rule("/api/menucategory/<id>", view_func=MenuCategory.deleteMenuCategoryById, methods=['delete'])

app.add_url_rule("/api/menu/create/", view_func=Menu.createTableMenu)
app.add_url_rule("/api/menus", view_func=Menu.getMenus)
app.add_url_rule("/api/menus/cat_id/<menucategory_id>", view_func=MenuCategory.getMenusByMenuCategoryId)
app.add_url_rule("/api/menus/cat_name/<menucategory_name>", view_func=MenuCategory.getMenusByMenuCategoryName)
app.add_url_rule("/api/menu/<id>", view_func=Menu.searchMenuById)
app.add_url_rule("/api/menu", view_func=Menu.createMenu, methods=['post'])
app.add_url_rule("/api/menu/<id>", view_func=Menu.updateMenu, methods=['put'])
app.add_url_rule("/api/menu/<id>", view_func=Menu.deleteMenuById, methods=['delete'])
