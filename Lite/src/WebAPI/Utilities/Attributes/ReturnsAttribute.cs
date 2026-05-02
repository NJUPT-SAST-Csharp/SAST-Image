using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Utilities.Attributes;

public class ReturnsAttribute<T>(int statusCode) : ProducesResponseTypeAttribute<T>(statusCode) { }

public class ReturnsAttribute(int statusCode) : ProducesResponseTypeAttribute(statusCode) { }
