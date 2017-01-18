package com.github.kazenetu.jerseyServer.resource;

import java.io.UnsupportedEncodingException;
import java.net.URLEncoder;
import java.util.ArrayList;
import java.util.List;

import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Response;

import com.github.kazenetu.jerseyServer.entity.TestData;

//以下でアクセス http://localhost:8080/jerseyServer/app/dl/download
@Path("dl")
public class DownloadResource {
    @GET
    @Path("download")
    @Produces("text/csv")
    public Response SendData(){
        List<TestData> list =new ArrayList<>();
        for(int i=0;i<10;i++){
        	list.add(new TestData("Name" + i, 20 + i));
        }

        String fileName = "テスト.csv";
        try {
			return Response.ok(list).header("Content-Disposition", "attachment; filename=" +  URLEncoder.encode(fileName , "utf-8")).build();
		} catch (UnsupportedEncodingException e) {
			// TODO 自動生成された catch ブロック
			return Response.serverError().build();
		}
    }

}
