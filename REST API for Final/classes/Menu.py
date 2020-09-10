from sqlalchemy.orm import relationship

from header import *
from flask_jwt_extended import (jwt_required)


class Menu(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(100))
    price = db.Column(db.Float)
    image = db.Column(db.String(250))
    menucategory_id = db.Column(db.Integer, db.ForeignKey('menu_category.id'), nullable=False)

    # create table
    @staticmethod
    def createTableMenu():
        db.create_all()
        return jsonify({"message": "table is created!"})

    # create menu
    @staticmethod
    @jwt_required
    def createMenu():
        data = request.get_json()

        obj_menu = Menu()
        obj_menu.name = data['name']
        obj_menu.price = data['price']
        obj_menu.image = data['image']
        obj_menu.menucategory_id = data['menucategory_id']

        db.session.add(obj_menu)
        db.session.commit()

        return jsonify({"message": "Menu is created!"})

    # select obj_menus without constraint
    @staticmethod
    @jwt_required
    def getMenus():
        obj_menus = Menu.query.all()
        result = []
        for col in obj_menus:
            menu_dict = {"id": col.id, "name": col.name, "price": col.price, "menucategory_id": col.menucategory_id, "image": col.image}
            result.append(menu_dict)
        return jsonify(result)

    # delete menu
    @staticmethod
    @jwt_required
    def deleteMenuById(id):
        obj_menu = Menu.query.filter_by(id=id).first()
        if not obj_menu:
            return jsonify({"message": "Menu is not found!"})
        db.session.delete(obj_menu)
        db.session.commit()
        return jsonify({"message": "Menu is deleted!"})

    # update menu
    @staticmethod
    @jwt_required
    def updateMenu(id):
        obj_menu = Menu.query.filter_by(id=id).first()
        data = request.get_json()

        if not obj_menu:
            return jsonify({"message": "Menu is not found!"})

        obj_menu.name = data['name']
        obj_menu.price = data['price']
        obj_menu.menucategory_id = data['menucategory_id']

        if data['image'] != "none":
            print("hello")
            print(data['image'])
            obj_menu.image = data['image']

        db.session.commit()

        return jsonify({"message": "Menu is updated!"})

    # search menu by id
    @staticmethod
    @jwt_required
    def searchMenuById(id):
        obj_menus = Menu.query.filter_by(id=id)
        result = []
        for col in obj_menus:
            menu_dict = {"id": col.id, "name": col.name, "price": col.price, "menucategory_id": col.menucategory_id, "image": col.image}
            result.append(menu_dict)
        return jsonify(result)

