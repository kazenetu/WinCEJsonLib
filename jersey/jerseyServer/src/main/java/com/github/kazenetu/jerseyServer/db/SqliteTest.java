package com.github.kazenetu.jerseyServer.db;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class SqliteTest {

	private Connection con = null;

	public SqliteTest(String filePath) {
		try {
			// JDBCドライバーの指定
			Class.forName("org.sqlite.JDBC");

			// データベースに接続する なければ作成される
			con = DriverManager.getConnection("jdbc:sqlite:"+filePath);

		} catch (ClassNotFoundException e) {
			// TODO 自動生成された catch ブロック
			e.printStackTrace();
		} catch (SQLException e) {
			// TODO 自動生成された catch ブロック
			// Connection の例外が発生した時

			e.printStackTrace();
		}
	}

	public boolean Login(String id){
		String sql = "select count(USER_ID) as cnt from MT_USER where USER_ID=?;";

		PreparedStatement statement;
		try {
			statement = con.prepareStatement(sql);

			statement.setString(1, id);
			ResultSet result = statement.executeQuery();

			if(result.getInt("cnt")>0){
				return true;
			}
		} catch (SQLException e) {
			// TODO 自動生成された catch ブロック
			e.printStackTrace();
		}

		return false;
	}

}