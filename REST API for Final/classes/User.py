from header import *
from flask_jwt_extended import (
    create_access_token, create_refresh_token, jwt_refresh_token_required, get_jwt_identity, jwt_required
)


class User(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    public_id = db.Column(db.String(100), unique=True)
    name = db.Column(db.String(100))
    password = db.Column(db.String(100))
    admin = db.Column(db.String(10))
    address = db.Column(db.String(100))

    # create table
    @staticmethod
    def index():
        db.create_all()
        return jsonify({"message": "table is created!"})

    # create user
    @staticmethod
    @jwt_required
    def createUser():
        data = request.get_json()

        obj_user = User()
        obj_user.public_id = str(uuid.uuid4())
        obj_user.name = data['name']

        hashed_password = generate_password_hash(data["password"], method="sha256")
        obj_user.password = hashed_password

        obj_user.admin = data['admin']
        obj_user.address = data['address']

        db.session.add(obj_user)
        db.session.commit()

        return jsonify({"message": "User is created!"})

    # select users without constraint
    @staticmethod
    @jwt_required
    def getUser():
        obj_users = User.query.all()
        result = []
        for col in obj_users:
            user_dict = {"id": col.id, "public_id": col.public_id, "name": col.name, "admin": col.admin, "password": col.password, "address": col.address}
            result.append(user_dict)
        return jsonify(result)

    # delete user
    @staticmethod
    @jwt_required
    def deleteUserByPublicId(publicId):
        obj_user = User.query.filter_by(public_id=publicId).first()
        if not obj_user:
            return jsonify({"message": "User is not found!"})
        db.session.delete(obj_user)
        db.session.commit()
        return jsonify({"message": "User is deleted!"})

    # update user
    @staticmethod
    @jwt_required
    def updateUser(publicId):
        obj_user = User.query.filter_by(public_id=publicId).first()
        data = request.get_json()

        if not obj_user:
            return jsonify({"message": "User is not found!"})

        obj_user.name = data['name']
        obj_user.admin = data['admin']
        obj_user.address = data['address']

        db.session.commit()

        return jsonify({"message": "User is updated!"})

    # search user by public_id
    @staticmethod
    @jwt_required
    def searchUserByPublicId():
        data = request.get_json()
        obj_users = User.query.filter_by(public_id=data['public_id'])
        print(data)
        result = []
        for col in obj_users:
            user_dict = {"id": col.id, "public_id": col.public_id, "name": col.name, "admin": col.admin, "password": col.password, "address": col.address}
            result.append(user_dict)
        # return json.dumps(result)
        return jsonify(result)

    # user login
    @staticmethod
    def Login():
        User.message = []
        data = request.get_json()

        objUser1 = User()
        objUser1.name = data["name"]
        objUser1.password = data["password"]

        objUser = User.query.filter_by(name=objUser1.name, admin='true').first()
        if not objUser:
            User.message_dict = {"message": "Username and password is invalid"}
            User.message.append(User.message_dict)
            return json.dumps(User.message), 401

        if not check_password_hash(objUser.password, objUser1.password):
            User.message_dict = {"message": "Username and password is invalid"}
            User.message.append(User.message_dict)
            return json.dumps(User.message), 401

        result = []

        ret = {'access_token': create_access_token(identity=objUser1.public_id),
               'refresh_token': create_refresh_token(identity=objUser1.public_id)}

        result.append(ret)

        return json.dumps(result), 200

    @staticmethod
    @jwt_refresh_token_required
    def refresh():
        current_user = get_jwt_identity()
        ret = {'access_token': create_access_token(identity=current_user)}
        return jsonify(ret), 200

    @ staticmethod
    @jwt_required
    def protected():
        username = get_jwt_identity()
        return jsonify(logged_in_as=username), 200
