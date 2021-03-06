PGDMP         (        
        x            flask_tutorial    12.3    12.3     $           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            %           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            &           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            '           1262    16393    flask_tutorial    DATABASE     �   CREATE DATABASE flask_tutorial WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'English_United States.1252' LC_CTYPE = 'English_United States.1252';
    DROP DATABASE flask_tutorial;
                postgres    false            �            1259    16404    alembic_version    TABLE     X   CREATE TABLE public.alembic_version (
    version_num character varying(32) NOT NULL
);
 #   DROP TABLE public.alembic_version;
       public         heap    postgres    false            �            1259    16429    menu    TABLE     �   CREATE TABLE public.menu (
    id integer NOT NULL,
    name character varying(100),
    price double precision,
    image character varying(250),
    menucategory_id integer NOT NULL
);
    DROP TABLE public.menu;
       public         heap    postgres    false            �            1259    16421    menu_category    TABLE     `   CREATE TABLE public.menu_category (
    id integer NOT NULL,
    name character varying(100)
);
 !   DROP TABLE public.menu_category;
       public         heap    postgres    false            �            1259    16419    menu_category_id_seq    SEQUENCE     �   CREATE SEQUENCE public.menu_category_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 +   DROP SEQUENCE public.menu_category_id_seq;
       public          postgres    false    206            (           0    0    menu_category_id_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE public.menu_category_id_seq OWNED BY public.menu_category.id;
          public          postgres    false    205            �            1259    16427    menu_id_seq    SEQUENCE     �   CREATE SEQUENCE public.menu_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 "   DROP SEQUENCE public.menu_id_seq;
       public          postgres    false    208            )           0    0    menu_id_seq    SEQUENCE OWNED BY     ;   ALTER SEQUENCE public.menu_id_seq OWNED BY public.menu.id;
          public          postgres    false    207            �            1259    16411    user    TABLE     �   CREATE TABLE public."user" (
    id integer NOT NULL,
    public_id character varying(100),
    name character varying(100),
    password character varying(100),
    admin character varying(10),
    address character varying(100)
);
    DROP TABLE public."user";
       public         heap    postgres    false            �            1259    16409    user_id_seq    SEQUENCE     �   CREATE SEQUENCE public.user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 "   DROP SEQUENCE public.user_id_seq;
       public          postgres    false    204            *           0    0    user_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.user_id_seq OWNED BY public."user".id;
          public          postgres    false    203            �
           2604    16432    menu id    DEFAULT     b   ALTER TABLE ONLY public.menu ALTER COLUMN id SET DEFAULT nextval('public.menu_id_seq'::regclass);
 6   ALTER TABLE public.menu ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    208    207    208            �
           2604    16424    menu_category id    DEFAULT     t   ALTER TABLE ONLY public.menu_category ALTER COLUMN id SET DEFAULT nextval('public.menu_category_id_seq'::regclass);
 ?   ALTER TABLE public.menu_category ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    205    206    206            �
           2604    16414    user id    DEFAULT     d   ALTER TABLE ONLY public."user" ALTER COLUMN id SET DEFAULT nextval('public.user_id_seq'::regclass);
 8   ALTER TABLE public."user" ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    203    204    204                      0    16404    alembic_version 
   TABLE DATA           6   COPY public.alembic_version (version_num) FROM stdin;
    public          postgres    false    202   �       !          0    16429    menu 
   TABLE DATA           G   COPY public.menu (id, name, price, image, menucategory_id) FROM stdin;
    public          postgres    false    208   �                 0    16421    menu_category 
   TABLE DATA           1   COPY public.menu_category (id, name) FROM stdin;
    public          postgres    false    206   �                  0    16411    user 
   TABLE DATA           O   COPY public."user" (id, public_id, name, password, admin, address) FROM stdin;
    public          postgres    false    204    !       +           0    0    menu_category_id_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public.menu_category_id_seq', 11, true);
          public          postgres    false    205            ,           0    0    menu_id_seq    SEQUENCE SET     :   SELECT pg_catalog.setval('public.menu_id_seq', 10, true);
          public          postgres    false    207            -           0    0    user_id_seq    SEQUENCE SET     :   SELECT pg_catalog.setval('public.user_id_seq', 10, true);
          public          postgres    false    203            �
           2606    16408 #   alembic_version alembic_version_pkc 
   CONSTRAINT     j   ALTER TABLE ONLY public.alembic_version
    ADD CONSTRAINT alembic_version_pkc PRIMARY KEY (version_num);
 M   ALTER TABLE ONLY public.alembic_version DROP CONSTRAINT alembic_version_pkc;
       public            postgres    false    202            �
           2606    16426     menu_category menu_category_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.menu_category
    ADD CONSTRAINT menu_category_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY public.menu_category DROP CONSTRAINT menu_category_pkey;
       public            postgres    false    206            �
           2606    16434    menu menu_pkey 
   CONSTRAINT     L   ALTER TABLE ONLY public.menu
    ADD CONSTRAINT menu_pkey PRIMARY KEY (id);
 8   ALTER TABLE ONLY public.menu DROP CONSTRAINT menu_pkey;
       public            postgres    false    208            �
           2606    16416    user user_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public."user" DROP CONSTRAINT user_pkey;
       public            postgres    false    204            �
           2606    16418    user user_public_id_key 
   CONSTRAINT     Y   ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_public_id_key UNIQUE (public_id);
 C   ALTER TABLE ONLY public."user" DROP CONSTRAINT user_public_id_key;
       public            postgres    false    204            �
           2606    16435    menu menu_menucategory_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.menu
    ADD CONSTRAINT menu_menucategory_id_fkey FOREIGN KEY (menucategory_id) REFERENCES public.menu_category(id);
 H   ALTER TABLE ONLY public.menu DROP CONSTRAINT menu_menucategory_id_fkey;
       public          postgres    false    206    208    2713                  x������ � �      !     x�}��n� E��� ���}�,Ǒ�M��]T��c�������jV��G��ܑQ��(HIvO ;+�Md´�
A�f~����f��SD�� U�)�Ė�`@m������:]1ʁU���s"8� �#�x��r�;ǋ<���9����������#{�H6�N��}HǩyM�.�: �<�����R�&)��i���5��%}$����Y�E���(����d��ɵt�ݪ]v�v��|���]�xU��R�/~�a         8   x�3��*�LN�2�,�OI�2�����V(IM�2�L�OKKM�24�,��K����� Q�         �  x�u��n�1F����,kd��^B+�
�".b�fl���$)�HЧ�"
;$o,Y3���`)TG!�!��@T X�Rt�`���n�O��qc���wt�������c�1Sa��[B4d�� ��KK�C����Ԁ��?}9��xx��x����G�oV�d�T��=P�
Cֆ�<*����a�o��3ث����&�-��=E�PP�E�TX��fJ�5��ּ�3h�f�Z&`?�}��rEKT�k	0|B�$DpN���e�vx<n�w��=�i��߼�|��R�h8��lT+�kRI��F-�et5i����F���S$Hv���]=���x�ψF�U[��#��<�HjQ�L�������W�_����ui1�Tn�hե�8$�{E��O��Mc�i�87h�(D'�ۿi�δ./���E͛F)�Ħ��1��,�(8�T�3�s��}|jk�k,�h̽P�aC������>O���%mc�j�\U�w������j����[     