# 关于使用 VS2022 打开 VS2012 项目的问题

```
1. 最终可以打开并编译运行代码，也可以运行 T4 模板，但是需要手动逐个项目安装：
	1. Install-Package Microsoft.NETFramework.ReferenceAssemblies.net45 -Version 1.0.2
	2. 因为 VS2022 不再对 .net 4.5 作出支持
2. 可以创建 Ado.net 实体对象模型，一切正常
3. 最终可以运行，但是仍需要一个针对 VS2022 的优化版
```

# 关于 github 不再支持使用密码 push 的处理方案：

## github 上的设置
1. 进入 Settings  Developer settings
2. 点击  Generate a personal access token
3. 输入密码验证当前身份
4. 在下方选择需要的操作权限以及期限
5. 生成 token，格式如：???_????????????????????????????????????

## git push 前的操作（单个仓储的一次性操作）
1. git remote set-url origin https://???_????????????????????????????????????@github.com/ORG-STUDY-DOTNET/REP_STUDY_VS2022.git

# .net core 版 model 项目代码搭建（MySQL版）

## 1. 安装 VS2022
```
选择 Asp.net Web 那一项进行安装，其它无需选择
```

## 2. 本地部署 MySQL 
```
(见 https://github.com/ORG-STUDY-MYSQL/REP_STUDY_MYSQL5731/blob/main/001_%E6%9C%AC%E5%9C%B0%E9%83%A8%E7%BD%B2%E6%AD%A5%E9%AA%A4.md)
```
## 3. 移除不需要的项目及其文件，仅保留sln

## 4. 设定 mysql 数据库的 root 密码
```
1. 管理员启动 cmd，输入：net stop mysql
2. 使用 mysqld --skip-grant-tables 命令临时启动 mysql 服务，之后会阻塞这个窗口。
3. 使用普通身份启动 cmd，输入 mysql -u root
4. 使用 update 语句修改密码：update mysql.user set authentication_string=password('???') where user='root';
5. 关闭两个 cmd 窗口，再手动结束 mysqld 进程，确保 mysql 及 mysqld 进程全部结束。（普通cmd窗口可以直接关闭，对于管理员cmd窗口，可以直接停止mysqld 服务，自动解除阻塞）
```

## 5. 因为暂未启动 mysql 服务，这里顺便修改一下成为表名大小写敏感。
```
1. 打开 my.ini 文件
2. 在最后添加两行内容：
character_set_server=utf8
lower_case_table_names=2
3. 删除服务：sc delete MySQL
4. 安装服务：mysqld --install MySQL --defaults-file="C:\Program Files\mysql-5.7.19-winx64\my.ini"
5. 在“服务”中，右键启动 MySQL 看是否成功
```

## 6. 创建库和表（这里使用普通身份登录 mysql）
```
1. create database studyvs2022 character set utf8 collate utf8_general_ci;
2. use studyvs2022;
3. 表：TUser
create table TUser
(
	TU_GUID varchar(50)   primary key,
	TU_Account varchar(20)		null,
	TU_Password varchar(255)   null,
	TU_RealName varchar(255)   null
);
4. 表 TOrder
create table TOrder
(
	TO_GUID varchar(50)  primary key,
	TO_Price int null
);
```

## 7. 在解决方案中创建类库（.net 6.0 版）
```
1. 名称为：Study.VS2022.Model
2. 删除不需要的类
3. 从 Nuget 中安装：Pomelo.EntityFrameworkCore.MySql 6.0.1
4. 从 Nuget 中安装：Microsoft.EntityFrameworkCore.Design 6.0.5
```

## 8. 根据表，生成实体：
```
1. 进入 Model 项目的文件夹，安装 dotnet-ef 工具： dotnet tool install --global dotnet-ef
2. 在该文件夹内，使用以下命令生成实体：（注意密码）
dotnet ef dbcontext scaffold "server=localhost;uid=root;pwd=???;port=3306;database=studyvs2022;" "Pomelo.EntityFrameworkCore.MySql" -c TestContext -o AutoModels -f
错误：这里提示：The framework 'Microsoft.NETCore.App', version '2.0.0' (x64) was not found.
（这里生成一下解决方案，再执行就可以了）
```

## 9. 生成的类名与属性与数据库中保持一致：
```
添加  --use-database-names：
dotnet ef dbcontext scaffold "server=localhost;uid=root;pwd=???;port=3306;database=studyvs2022;" "Pomelo.EntityFrameworkCore.MySql" --use-database-names -c TestContext -o AutoModels -f
```

## 10. 禁用引用类型的可空类型
```
在项目属性的“生成-常规”中修改，禁用“可为 Null 的类型”
```

# .net core 版 model 项目代码搭建（非 MySQL 版）
## 1. 下载并安装工具：（关键字：EF Core Power Tools，也可以在阿里云盘上搜索：EF Core Power Tools v2.5.1005）
## 2. 打开 VS2022，项目上右键，EF Core 工具，反向工程
## 3. 点击添加，输入数据库信息，左下角保持 EF Core 6，一路点击确定，直到出现选表的界面
## 4. 选择表，点击确定
## 5. 四个地方都输入名称：EFCorePowerTools
## 6. 勾选：使用数据库中的表名和列名、在项目中安装 EF Core 包
## 7. 直接点击确定
## 8. 生成解决方案测试

## 针对 Oracle
```
1. 只能使用添加 ad hoc 连接
2. 可以查看一下 tnanames.ora 文件，查看一下 DataSource = （）小括号里面填写什么，例：
Data Source=(DESCRIPTION =    (ADDRESS_LIST =      (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))    )    (CONNECT_DATA =      (SERVICE_NAME = orcl)    ));User Id=system;Password=system;
3. 注意，可以使用双引号编写 Oracle 的创建表的 SQL 语句，这样，在 EF Core 工具中再选中“使用数据库中的表名及列名”，就可以保证大小写一致了！
```
