#Adding Grpc client reference when using VS code and .NET SDK (Without Visual Studio)
Add the folllwing nuget package to the BasketAPI.csproj
 <PackageReference Include="Grpc.AspNetCore" Version="2.60.0" />
Add the following line to BasketAPI.csproj
  <Protobuf Include="..\Discount\Protos\discount.proto" GrpcServices="Client" >
    <Link>Protos\discount.proto</Link>
  </Protobuf>
