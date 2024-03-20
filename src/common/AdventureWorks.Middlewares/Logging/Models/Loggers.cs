﻿namespace AdventureWorks.Middlewares.Logging.Models;

public class SalesLogs(string? scheme, 
                       string? host, 
                       string? path, 
                       string? method, 
                       string? query, 
                       IDictionary<string, StringValues>? headers, 
                       IDictionary<string, string>? cookie, 
                       string? contentType, 
                       string? remoteIpAddress, 
                       string? body) : 
             BaseLog(scheme, 
                     host, 
                     path, 
                     method, 
                     query, 
                     headers, 
                     cookie, 
                     contentType, 
                     remoteIpAddress, 
                     body);

public class ProductionLogs(string? scheme, 
                            string? host, 
                            string? path, 
                            string? method, 
                            string? query, 
                            IDictionary<string, StringValues>? headers, 
                            IDictionary<string, string>? cookie, 
                            string? contentType, 
                            string? remoteIpAddress, 
                            string? body) : 
             BaseLog(scheme, 
                     host, 
                     path, 
                     method, 
                     query, 
                     headers, 
                     cookie, 
                     contentType, 
                     remoteIpAddress, 
                     body);