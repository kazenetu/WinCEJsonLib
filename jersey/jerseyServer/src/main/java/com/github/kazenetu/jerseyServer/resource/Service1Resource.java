package com.github.kazenetu.jerseyServer.resource;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import javax.servlet.ServletContext;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.MediaType;

import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.github.kazenetu.jerseyServer.db.SqliteTest;
import com.github.kazenetu.jerseyServer.entity.TestData;
import com.github.kazenetu.jerseyServer.entity.TestDataCount;

//以下でアクセス http://localhost:8080/jerseyServer/app/Service1
@Path("Service1")
public class Service1Resource {
	private ServletContext context;

    @GET
    @Path("SendData")
    @Produces(MediaType.APPLICATION_JSON)
    public String SendData(){
        List<TestData> list =new ArrayList<>();
        for(int i=0;i<10;i++){
        	list.add(new TestData("Name" + i, 20 + i));
        }

        String json ="";
        ObjectMapper mapper = new ObjectMapper();
        try {
			json = mapper.writeValueAsString(list);
		} catch (JsonProcessingException e) {
			// TODO 自動生成された catch ブロック
			e.printStackTrace();
		}

        return json;
    }

    @POST
    @Path("GetDataCount")
    //@Consumes(MediaType.APPLICATION_JSON)
    @Produces(MediaType.APPLICATION_JSON)
    public String GetDataCount(String json){

        TestDataCount result =new TestDataCount();
        result.count=0;

    	ObjectMapper mapper = new ObjectMapper();

    	List<TestData> list;
		try {
			list = mapper.readValue(json, new TypeReference<List<TestData>>() {});
	        result.count=list.size();
		} catch (JsonParseException e) {
			// TODO 自動生成された catch ブロック
			e.printStackTrace();
		} catch (JsonMappingException e) {
			// TODO 自動生成された catch ブロック
			e.printStackTrace();
		} catch (IOException e) {
			// TODO 自動生成された catch ブロック
			e.printStackTrace();
		}


        try {
			json = mapper.writeValueAsString(result);
		} catch (JsonProcessingException e) {
			// TODO 自動生成された catch ブロック
			e.printStackTrace();
		}

        return json;
    }

    @Context
    public void setServletContext(ServletContext context) {
        this.context = context;
    }

    @GET
    @Path("Login")
    @Produces(MediaType.TEXT_PLAIN)
    public String Login(@QueryParam("id") String id){

		String filePath=this.context.getRealPath("/WEB-INF/classes/Test.db");


		SqliteTest test = new SqliteTest(filePath);

    	if(test.Login(id)){
    		return "OK";
    	}

    	return "NG";
    }
}
