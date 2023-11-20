﻿using Core.Dtos.Project;
using Core.Dtos.Tier;
using Domain.Features.ProjectFeatures.Commands;
using Microsoft.AspNetCore.Mvc;
using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Fluent;
using ConfigurationBuilder = Reinforced.Typings.Fluent.ConfigurationBuilder;

namespace CrowdFundingAPI.Typings;

public static class ReinforcedTypingsConfiguration
{
    public static void Configure(ConfigurationBuilder builder)
    {
        builder.ExportAsInterfaces(
                new Type[] {    
                    typeof(ProjectDto), 
                    typeof(ProjectWithTiersDto), 
                    typeof(PublishedProjectDto),
                    typeof(TierDto),
                    typeof(CreateProjectCommand),
                    typeof(UpdateProjectCommand),
                    typeof(DeleteProjectCommand),
                    typeof(PublishProjectCommand),
                    typeof(SupportProjectCommand),
                },
                conf => conf.WithPublicProperties()
            );
        
        builder.Global(c =>
            c.UseModules()
                .CamelCaseForProperties()
                .CamelCaseForMethods()
                .UseVisitor<Visitor>());
        
        builder.SubstituteGeneric(
            typeof(ActionResult<>),
            (t, r) => r.ResolveTypeName(t.GenericTypeArguments.First()));
        builder.SubstituteGeneric(
            typeof(Nullable<>),
            (t, r) => new RtSimpleTypeName(
                $"{r.ResolveTypeName(t.GenericTypeArguments.First())} | null"));
        
        builder.Substitute(typeof(DateTime), new RtSimpleTypeName("Date"));
        builder.Substitute(typeof(ActionResult), new RtSimpleTypeName("void"));
        builder.Substitute(typeof(IActionResult), new RtSimpleTypeName("void"));
        builder.Substitute(typeof(CancellationToken), new RtSimpleTypeName("unknown | null"));
    }
}