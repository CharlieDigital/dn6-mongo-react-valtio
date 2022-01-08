global using System.Text.Json;

global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;

global using MongoDB.Driver;
global using MongoDB.Driver.Linq;
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;
global using MongoDB.Bson.Serialization.IdGenerators;

global using Serilog;

global using Api.Support;
global using Api.Domain.Core;
global using Api.Domain.Model;
global using Api.DataAccess.Core;
global using Api.DataAccess.Support;