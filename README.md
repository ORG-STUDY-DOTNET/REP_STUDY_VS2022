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

# .net core 版项目代码搭建

## 1. 安装 VS2022
```
选择 Asp.net Web 那一项进行安装，其它无需选择
```

## 2. 本地部署 MySQL 
```
(见 https://github.com/ORG-STUDY-MYSQL/REP_STUDY_MYSQL5731/blob/main/001_%E6%9C%AC%E5%9C%B0%E9%83%A8%E7%BD%B2%E6%AD%A5%E9%AA%A4.md)
```
