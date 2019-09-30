﻿using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.Application.Cqs.Commands.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Application.Cqs.Commands.Definitions
{
    public class UpdateInputCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid Icon { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public string Instruction { get; private set; }
        public List<ImageDto> Images { get; private set; }
        public List<VideoDto> Videos { get; private set; }
        public int Version { get; private set; }

        public UpdateInputCommand()
        {
        }

        public UpdateInputCommand(Guid id,
                                  string name,
                                  string description,
                                  Guid icon,
                                  IEnumerable<string> tags,
                                  string instruction,
                                  List<ImageDto> images,
                                  List<VideoDto> videos,
                                  int version) : this()
        {
            Id = id;
            Name = name;
            Description = description;
            Icon = icon;
            Tags = tags ?? new List<string>();
            Instruction = instruction;
            Images = images;
            Videos = videos;
            Version = version;
        }

    }
}
