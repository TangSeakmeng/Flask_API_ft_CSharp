from sqlalchemy.orm import relationship
from classes.Menu import *

from header import *
from flask_jwt_extended import (
    create_access_token, create_refresh_token, jwt_refresh_token_required, get_jwt_identity, jwt_required
)


class MenuCategory(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(100))
    menus = db.relationship('Menu', backref='menucategory_menu', lazy=True)

    # create table
    @staticmethod
    def createTableMenuCategory():
        db.create_all()
        return jsonify({"message": "table is created!"})

    # create menu category
    @staticmethod
    @jwt_required
    def createMenuCategory():
        data = request.get_json()

        obj_menuCategory = MenuCategory()
        obj_menuCategory.name = data['name']

        db.session.add(obj_menuCategory)
        db.session.commit()

        return jsonify({"message": "MenuCategory is created!"})

    # select menu categories without constraint
    @staticmethod
    @jwt_required
    def getMenuCategories():
        obj_menuCategories = MenuCategory.query.all()
        result = []
        for col in obj_menuCategories:
            menuCategory_dict = {"id": col.id, "name": col.name}
            result.append(menuCategory_dict)
        return jsonify(result)

    @staticmethod
    @jwt_required
    def getMenusByMenuCategoryName(menucategory_name):
        obj_menuCategories = MenuCategory.query.filter_by(name=menucategory_name).join(Menu, MenuCategory.id == Menu.menucategory_id).all()
        result = []
        for col in obj_menuCategories:
            for col2 in col.menus:
                menu_dict = {"id": col2.id, "name": col2.name, "price": col2.price, "image": col2.image}
                result.append(menu_dict)
        return jsonify(result)

    # get menu by category id
    @staticmethod
    @jwt_required
    def getMenusByMenuCategoryId(menucategory_id):
        obj_menuCategories = MenuCategory.query.filter_by(id=menucategory_id).join(Menu,MenuCategory.id == Menu.menucategory_id).all()
        result = []
        for col in obj_menuCategories:
            for col2 in col.menus:
                menu_dict = {"id": col2.id, "name": col2.name, "price": col2.price, "image": col2.image}
                result.append(menu_dict)
        return jsonify(result)

    # delete menucategory
    @staticmethod
    @jwt_required
    def deleteMenuCategoryById(id):
        obj_menuCategory = MenuCategory.query.filter_by(id=id).first()
        if not obj_menuCategory:
            return jsonify({"message": "MenuCategory is not found!"})
        db.session.delete(obj_menuCategory)
        db.session.commit()
        return jsonify({"message": "MenuCategory is deleted!"})

    # update menucategory
    @staticmethod
    @jwt_required
    def updateMenuCategory(id):
        obj_menuCategory = MenuCategory.query.filter_by(id=id).first()
        data = request.get_json()

        if not obj_menuCategory:
            return jsonify({"message": "MenuCategory is not found!"})

        obj_menuCategory.name = data['name']

        db.session.commit()

        return jsonify({"message": "MenuCategory is updated!"})

    # search menu category by public_id
    @staticmethod
    @jwt_required
    def searchMenuCategoryById(id):
        data = request.get_json()
        obj_menuCategory = MenuCategory.query.filter_by(id=id)
        result = []
        for col in obj_menuCategory:
            menuCategory_dict = {"id": col.id, "name": col.name}
            result.append(menuCategory_dict)
        return jsonify(result)

    # search menu id by name
    @staticmethod
    @jwt_required
    def getCategoryIdFromCategoryName():
        data = request.get_json()
        obj_menuCategory = MenuCategory.query.filter_by(name=data['name'])
        menuCategory_id = 0;
        for col in obj_menuCategory:
            menuCategory_id = col.id;
        return jsonify(menuCategory_id)
