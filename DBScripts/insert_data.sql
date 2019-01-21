/*User*/
INSERT INTO [LMS].[dbo].[Users]([IdUser],[IsActive],[BirthDate],[Firstname],[Lastname],[Username],[UserPassword],[Email],[RefCategory],[RefUserCreatedBy],[DateTimeCreatedOn],[RefUserDeletedBy],[DateTimeDeletedOn])
VALUES
(1, -1, '1994-08-02 22:10:00.00', 'Admin', 'Admin', 'admin', 'admin', 'admin@gmail.com', NULL, NULL, GETDATE(), NULL, NULL),
(2, -1, '1984-08-22 22:10:00.00', 'Jane', 'Smith', 'jane', 'jane', 'jane@gmail.com', NULL, NULL, GETDATE(), NULL, NULL),
(3, -1, '1994-05-07 21:30:00.00', 'Mike', 'Gosling', 'mike123', 'mike123', 'mike@gmail.com', NULL, NULL, GETDATE(), NULL, NULL),
(4, -1, '1991-01-29 09:45:00.00', 'Sophie', 'Turner', 'sophiee', 'sophiee', 'sophieturner@gmail.com', NULL, NULL, GETDATE(), NULL, NULL),
(5, -1, '1991-01-29 09:45:00.00', 'Nick', 'Potter', 'nickpotter', 'nickpotter', 'nick@gmail.com', NULL, NULL, GETDATE(), NULL, NULL);

/*Role*/
INSERT INTO [LMS].[dbo].[Roles]([IdRole],[IsActive],[NameRole],[CodeRole],[RefUserCreatedBy],[DateTimeCreatedOn],[RefUserDeletedBy],[DateTimeDeletedOn])
VALUES
(1, -1, 'admin', 'Admin', 1, GETDATE(), NULL, NULL),
(2, -1, 'librarian', 'Librarian', 1, GETDATE(), NULL, NULL),
(3, -1, 'subscriber', 'Subscriber', 1, GETDATE(), NULL, NULL);

/*UserRole*/
INSERT INTO [LMS].[dbo].[RelationUserRole]([IdRelationUserRole],[IsActive],[RefUser],[RefRole],[RefUserCreatedBy],[DateTimeCreatedOn],[RefUserDeletedBy],[DateTimeDeletedOn])
VALUES
(1, -1, 1, 1, NULL, GETDATE(), NULL, NULL),
(2, -1, 2, 2, 1, GETDATE(), NULL, NULL),
(3, -1, 3, 2, 1, GETDATE(), NULL, NULL),
(4, -1, 4, 3, 1, GETDATE(), NULL, NULL),
(5, -1, 5, 3, 1, GETDATE(), NULL, NULL);

/*Permission*/
INSERT INTO [LMS].[dbo].[Permission]([IdPermission],[IsActive],[NamePermission],[CodePermission],[RefUserCreatedBy],[DateTimeCreatedOn],[RefUserDeletedBy],[DateTimeDeletedOn])
VALUES
(1, -1, 'View overview book', 'ViewOverviewBook', 1, GETDATE(), NULL, NULL),
(2, -1, 'Deny view overview book', 'DenyViewOverviewBook', 1, GETDATE(), NULL, NULL),
(3, -1, 'View overview category', 'ViewOverviewCategory', 1, GETDATE(), NULL, NULL),
(4, -1, 'Deny view overview category', 'DenyViewOverviewCategory', 1, GETDATE(), NULL, NULL),
(5, -1, 'View overview language', 'ViewOverviewLanguage', 1, GETDATE(), NULL, NULL),
(6, -1, 'Deny view overview language', 'DenyViewOverviewLanguage', 1, GETDATE(), NULL, NULL),
(7, -1, 'View overview users', 'ViewOverviewUsers', 1, GETDATE(), NULL, NULL),
(8, -1, 'Deny view overview users', 'DenyViewOverviewUsers', 1, GETDATE(), NULL, NULL),
(9, -1, 'View overview librarians', 'ViewOverviewLibrarians', 1, GETDATE(), NULL, NULL),
(10, -1, 'Deny view overview librarians', 'DenyViewOverviewLibrarians', 1, GETDATE(), NULL, NULL),
(11, -1, 'View overview subscribers', 'ViewOverviewSubscribers', 1, GETDATE(), NULL, NULL),
(12, -1, 'Deny view overview subscribers', 'DenyViewOverviewSubscribers', 1, GETDATE(), NULL, NULL),
(13, -1, 'View overview ebook', 'ViewOverviewEBook', 1, GETDATE(), NULL, NULL),
(14, -1, 'Deny view overview ebook', 'DenyViewOverviewEBook', 1, GETDATE(), NULL, NULL),
(15, -1, 'View add book', 'ViewAddBook', 1, GETDATE(), NULL, NULL),
(16, -1, 'Deny view add book', 'DenyViewAddBook', 1, GETDATE(), NULL, NULL),
(17, -1, 'View add category', 'ViewAddCategory', 1, GETDATE(), NULL, NULL),
(18, -1, 'Deny view add category', 'DenyViewAddCategory', 1, GETDATE(), NULL, NULL),
(19, -1, 'View add language', 'ViewAddLanguage', 1, GETDATE(), NULL, NULL),
(20, -1, 'Deny view add language', 'DenyViewAddLanguage', 1, GETDATE(), NULL, NULL),
(21, -1, 'View add user', 'ViewAddUser', 1, GETDATE(), NULL, NULL),
(22, -1, 'Deny view add user', 'DenyViewAddUser', 1, GETDATE(), NULL, NULL),
(23, -1, 'View add ebook', 'ViewAddEBook', 1, GETDATE(), NULL, NULL),
(24, -1, 'Deny view add ebook', 'DenyViewAddEBook', 1, GETDATE(), NULL, NULL),
(25, -1, 'View add subscriber', 'ViewAddSubscriber', 1, GETDATE(), NULL, NULL),
(26, -1, 'Deny view add subscriber', 'DenyViewAddSubscriber', 1, GETDATE(), NULL, NULL),
(27, -1, 'View add librarian', 'ViewAddLibrarian', 1, GETDATE(), NULL, NULL),
(28, -1, 'Deny view add librarian', 'DenyViewAddLibrarian', 1, GETDATE(), NULL, NULL),
(29, -1, 'View search ebook', 'ViewSearchEBook', 1, GETDATE(), NULL, NULL),
(30, -1, 'Deny view search ebook', 'DenyViewSearchEBook', 1, GETDATE(), NULL, NULL),
(31, -1, 'Edit category', 'EditCategory', 1, GETDATE(), NULL, NULL),
(32, -1, 'Deny edit category', 'DenyEditCategory', 1, GETDATE(), NULL, NULL),
(33, -1, 'Delete category', 'DeleteCategory', 1, GETDATE(), NULL, NULL),
(34, -1, 'Deny delete category', 'DenyDeleteCategory', 1, GETDATE(), NULL, NULL),
(35, -1, 'Edit book', 'EditBook', 1, GETDATE(), NULL, NULL),
(36, -1, 'Deny edit book', 'DenyEditBook', 1, GETDATE(), NULL, NULL),
(37, -1, 'Delete book', 'DeleteBook', 1, GETDATE(), NULL, NULL),
(38, -1, 'Deny delete book', 'DenyDeleteBook', 1, GETDATE(), NULL, NULL),
(39, -1, 'Edit language', 'EditLanguage', 1, GETDATE(), NULL, NULL),
(40, -1, 'Deny edit language', 'DenyEditLanguage', 1, GETDATE(), NULL, NULL),
(41, -1, 'Delete language', 'DeleteLanguage', 1, GETDATE(), NULL, NULL),
(42, -1, 'Deny delete language', 'DenyDeleteLanguage', 1, GETDATE(), NULL, NULL),
(43, -1, 'Edit user', 'EditUser', 1, GETDATE(), NULL, NULL),
(44, -1, 'Deny edit user', 'DenyEditUser', 1, GETDATE(), NULL, NULL),
(45, -1, 'Delete user', 'DeleteUser', 1, GETDATE(), NULL, NULL),
(46, -1, 'Deny delete user', 'DenyDeleteUser', 1, GETDATE(), NULL, NULL),
(47, -1, 'Make loan', 'MakeLoan', 1, GETDATE(), NULL, NULL),
(48, -1, 'Deny make loan', 'DenyMakeLoan', 1, GETDATE(), NULL, NULL),
(49, -1, 'Return loan', 'ReturnLoan', 1, GETDATE(), NULL, NULL),
(50, -1, 'Deny return loan', 'DenyReturnLoan', 1, GETDATE(), NULL, NULL),
(51, -1, 'View add copy', 'ViewAddCopy', 1, GETDATE(), NULL, NULL),
(52, -1, 'Deny view add copy', 'DenyViewAddCopy', 1, GETDATE(), NULL, NULL),
(53, -1, 'Delete copy', 'DeleteCopy', 1, GETDATE(), NULL, NULL),
(54, -1, 'Deny delete copy', 'DenyDeleteCopy', 1, GETDATE(), NULL, NULL),
(55, -1, 'View permission panel', 'ViewPermissionPanel', 1, GETDATE(), NULL, NULL),
(56, -1, 'Deny view permission panel', 'DenyViewPermissionPanel', 1, GETDATE(), NULL, NULL);


/*Role Permission -> admin part*/
INSERT INTO [LMS].[dbo].[RelationRolePermission]([IdRelationRolePermission],[IsActive],[RefRole],[RefPermission],[RefUserCreatedBy],[DateTimeCreatedOn],[RefUserDeletedBy],[DateTimeDeletedOn])
VALUES
(1, -1, 1, 1, 1, GETDATE(), NULL, NULL),
(2, -1, 1, 3, 1, GETDATE(), NULL, NULL),
(3, -1, 1, 5, 1, GETDATE(), NULL, NULL),
(4, -1, 1, 7, 1, GETDATE(), NULL, NULL),
(5, -1, 1, 9, 1, GETDATE(), NULL, NULL),
(6, -1, 1, 11, 1, GETDATE(), NULL, NULL),
(7, -1, 1, 13, 1, GETDATE(), NULL, NULL),
(8, -1, 1, 15, 1, GETDATE(), NULL, NULL),
(9, -1, 1, 17, 1, GETDATE(), NULL, NULL),
(10, -1, 1, 19, 1, GETDATE(), NULL, NULL),
(11, -1, 1, 21, 1, GETDATE(), NULL, NULL),
(12, -1, 1, 23, 1, GETDATE(), NULL, NULL),
(13, -1, 1, 25, 1, GETDATE(), NULL, NULL),
(14, -1, 1, 27, 1, GETDATE(), NULL, NULL),
(15, -1, 1, 29, 1, GETDATE(), NULL, NULL),
(16, -1, 1, 31, 1, GETDATE(), NULL, NULL),
(17, -1, 1, 33, 1, GETDATE(), NULL, NULL),
(18, -1, 1, 35, 1, GETDATE(), NULL, NULL),
(19, -1, 1, 37, 1, GETDATE(), NULL, NULL),
(20, -1, 1, 39, 1, GETDATE(), NULL, NULL),
(21, -1, 1, 41, 1, GETDATE(), NULL, NULL),
(22, -1, 1, 43, 1, GETDATE(), NULL, NULL),
(23, -1, 1, 45, 1, GETDATE(), NULL, NULL),
(24, -1, 1, 47, 1, GETDATE(), NULL, NULL),
(25, -1, 1, 49, 1, GETDATE(), NULL, NULL),
(26, -1, 1, 51, 1, GETDATE(), NULL, NULL),
(27, -1, 1, 53, 1, GETDATE(), NULL, NULL),
(28, -1, 1, 1, 2, GETDATE(), NULL, NULL), --librarian part
(29, -1, 1, 3, 2, GETDATE(), NULL, NULL),
(30, -1, 1, 5, 2, GETDATE(), NULL, NULL),
(31, -1, 1, 11, 2, GETDATE(), NULL, NULL),
(32, -1, 1, 13, 2, GETDATE(), NULL, NULL),
(33, -1, 1, 15, 2, GETDATE(), NULL, NULL),
(34, -1, 1, 17, 2, GETDATE(), NULL, NULL),
(35, -1, 1, 29, 2, GETDATE(), NULL, NULL),
(36, -1, 1, 31, 2, GETDATE(), NULL, NULL),
(37, -1, 1, 33, 2, GETDATE(), NULL, NULL),
(38, -1, 1, 35, 2, GETDATE(), NULL, NULL),
(39, -1, 1, 37, 2, GETDATE(), NULL, NULL),
(40, -1, 1, 47, 2, GETDATE(), NULL, NULL),
(41, -1, 1, 49, 2, GETDATE(), NULL, NULL),
(42, -1, 1, 51, 2, GETDATE(), NULL, NULL),
(43, -1, 1, 53, 2, GETDATE(), NULL, NULL),
(44, -1, 1, 1, 3, GETDATE(), NULL, NULL),--subscriber part
(45, -1, 1, 3, 3, GETDATE(), NULL, NULL),
(46, -1, 1, 5, 3, GETDATE(), NULL, NULL),
(47, -1, 1, 13, 3, GETDATE(), NULL, NULL),
(48, -1, 1, 29, 3, GETDATE(), NULL, NULL),
(49, -1, 1, 55, 1, GETDATE(), NULL, NULL);


/*Language*/
INSERT INTO [LMS].[dbo].[Languages]([IdLanguage],[IsActive],[NameLanguage],[CodeLanguage],[RefUserCreatedBy],[DateTimeCreatedOn],[RefUserDeletedBy],[DateTimeDeletedOn])
VALUES
(1, -1, 'English', 'en-US', 1, GETDATE(), NULL, NULL),
(2, -1, 'German', 'de-DE', 1, GETDATE(), NULL, NULL),
(3, -1, 'Serbian', 'sr-SR', 1, GETDATE(), NULL, NULL),
(4, -1, 'Italian', 'it-IT', 1, GETDATE(), NULL, NULL);


/*Category*/
INSERT INTO [LMS].[dbo].[Category]([IdCategory],[IsActive],[NameCategory],[CodeCategory],[RefUserCreatedBy],[DateTimeCreatedOn],[RefUserDeletedBy],[DateTimeDeletedOn])
VALUES
(1, -1, 'Art', 'cat_ART', 1, GETDATE(), NULL, NULL),
(2, -1, 'Biography', 'cat_Biography', 1, GETDATE(), NULL, NULL),
(3, -1, 'Business', 'cat_Business', 1, GETDATE(), NULL, NULL),
(4, -1, 'Kids', 'cat_Kids', 1, GETDATE(), NULL, NULL),  
(5, -1, 'Comic', 'cat_Comic', 1, GETDATE(), NULL, NULL),  
(6, -1, 'Computers', 'cat_Computers', 1, GETDATE(), NULL, NULL),  
(7, -1, 'Classics', 'cat_Classics', 1, GETDATE(), NULL, NULL),  
(8, -1, 'Contemporary', 'cat_Contemporary', 1, GETDATE(), NULL, NULL),  
(9, -1, 'Fiction', 'cat_Fiction', 1, GETDATE(), NULL, NULL),  
(10, -1, 'Mystery', 'cat_Mystery', 1, GETDATE(), NULL, NULL);

/*Books*/
INSERT INTO [LMS].[dbo].[Book]([IdBook],[IsActive],[IsElectronic],[NumOfAvailableCopies],[Title],[Author],[PublicationYear],[Keywords],[BookFilename],[MIME],[RefCategory],[RefLanguage],[RefCataloguer],[RefUserCreatedBy],[DateTimeCreatedOn],[RefUserDeletedBy],[DateTimeDeletedOn])
VALUES
(1, -1, 0, 1, 'World of Art', 'Henry M. Sayre', 1999, NULL, NULL, NULL, 1, 1, NULL, 1, GETDATE(), NULL, NULL),
(2, -1, 0, 3, 'The diary of a young girl', 'Anne Frank', 2005, NULL, NULL, NULL, 2, 1, NULL, 1, GETDATE(), NULL, NULL),
(3, -1, 0, 2, 'Kad je svet imao brkove', 'Ana Radmilović', 2017, NULL, NULL, NULL, 2, 3, NULL, 1, GETDATE(), NULL, NULL),
(4, -1, 0, 2, 'Steve Jobs', 'Walter Isaacson', 2011, NULL, NULL, NULL, 2, 1, NULL, 1, GETDATE(), NULL, NULL),
(5, -1, 0, 2, 'Blago cara Radovana', 'Jovan Dučić', 2018, NULL, NULL, NULL, 7, 3, NULL, 1, GETDATE(), NULL, NULL),   
(6, -1, 0, 4, 'Zločin i kazna', 'Fjodor M. Dostojevski', 2010, NULL, NULL, NULL, 7, 3, NULL, 1, GETDATE(), NULL, NULL),   
(7, -1, 0, 1, 'Clean Code', 'Robert C. Martin', 2015, NULL, NULL, NULL, 6, 1, NULL, 1, GETDATE(), NULL, NULL), 
(8, -1, 0, 1, 'Hello world', 'Hannah Fry', 2017, NULL, NULL, NULL, 6, 1, NULL, 1, GETDATE(), NULL, NULL), 
(9, -1, 0, 3, 'Lizalica iz Uagadugua', 'Renaug Kleiva', 2003, NULL, NULL, NULL, 4, 3, NULL, 1, GETDATE(), NULL, NULL), 
(10, -1, 0, 1, 'Moja genijalna prijateljica', 'Elena Ferante', 2017, NULL, NULL, NULL, 6, 3, NULL, 1, GETDATE(), NULL, NULL);


/*Books copies*/
INSERT INTO [LMS].[dbo].[BookCopy]([IdBookCopy],[IsActive],[RefBook],[OnLoan],[RefUserCreatedBy],[DateTimeCreatedOn],[RefUserDeletedBy],[DateTimeDeletedOn])
VALUES
(1, -1, 1, 0, 1, GETDATE(), NULL, NULL),
(2, -1, 2, 0, 1, GETDATE(), NULL, NULL),
(3, -1, 2, 0, 1, GETDATE(), NULL, NULL),
(4, -1, 2, 0, 1, GETDATE(), NULL, NULL),
(5, -1, 3, 0, 1, GETDATE(), NULL, NULL),
(6, -1, 3, 0, 1, GETDATE(), NULL, NULL),
(7, -1, 4, 0, 1, GETDATE(), NULL, NULL),
(8, -1, 4, 0, 1, GETDATE(), NULL, NULL),
(9, -1, 5, 0, 1, GETDATE(), NULL, NULL),
(10, -1, 5, 0, 1, GETDATE(), NULL, NULL),
(11, -1, 6, 0, 1, GETDATE(), NULL, NULL),
(12, -1, 6, 0, 1, GETDATE(), NULL, NULL),
(13, -1, 6, 0, 1, GETDATE(), NULL, NULL),
(14, -1, 6, 0, 1, GETDATE(), NULL, NULL),
(15, -1, 7, 0, 1, GETDATE(), NULL, NULL),
(16, -1, 8, 0, 1, GETDATE(), NULL, NULL),
(17, -1, 9, 0, 1, GETDATE(), NULL, NULL),
(18, -1, 9, 0, 1, GETDATE(), NULL, NULL),
(19, -1, 9, 0, 1, GETDATE(), NULL, NULL),
(20, -1, 10, 0, 1, GETDATE(), NULL, NULL); 