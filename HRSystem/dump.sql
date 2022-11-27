CREATE TABLE hrsystem.dbo.division (
	division_id int IDENTITY(1,1) NOT NULL,
	name nvarchar(100) COLLATE Cyrillic_General_CI_AS NOT NULL,
	parent_id int NULL,
	created_at datetime DEFAULT getdate() NULL,
	CONSTRAINT division_PK PRIMARY KEY (division_id)
);

CREATE TABLE hrsystem.dbo.employee (
	employee_id int IDENTITY(1,1) NOT NULL,
	first_name nvarchar(100) COLLATE Cyrillic_General_CI_AS NOT NULL,
	middle_name nvarchar(100) COLLATE Cyrillic_General_CI_AS NULL,
	last_name nvarchar(100) COLLATE Cyrillic_General_CI_AS NOT NULL,
	division_id int NULL,
	CONSTRAINT pk_employee PRIMARY KEY (employee_id),
	CONSTRAINT employee_FK FOREIGN KEY (division_id) REFERENCES hrsystem.dbo.division(division_id) ON DELETE SET NULL
);

CREATE TABLE hrsystem.dbo.transfer_history (
	transfer_id int IDENTITY(0,1) NOT NULL,
	division_id int NULL,
	employee_id int NULL,
	date_from datetime DEFAULT getdate() NULL,
	date_to datetime NULL,
	CONSTRAINT pk_transfer_history PRIMARY KEY (transfer_id),
	CONSTRAINT transfer_history_FK FOREIGN KEY (division_id) REFERENCES hrsystem.dbo.division(division_id) ON DELETE SET NULL,
	CONSTRAINT transfer_history_FK_1 FOREIGN KEY (employee_id) REFERENCES hrsystem.dbo.employee(employee_id) ON DELETE SET NULL
);