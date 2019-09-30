using ITG.Brix.EccSetup.Domain.Internal;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class Validation : BuildingBlock
    {
        private List<Tag> _tags = new List<Tag>();
        private List<Image> _images = new List<Image>();
        private List<Video> _videos = new List<Video>();
        private List<ValidationAttribute> _validationAttributes = new List<ValidationAttribute>();

        public string Name { get; private set; }
        public string NameOnApplication { get; private set; }
        public string Description { get; private set; }
        public string Instruction { get; private set; }
        public BuildingBlockIcon Icon { get; private set; }
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
        public IReadOnlyCollection<Image> Images => _images.AsReadOnly();
        public IReadOnlyCollection<Video> Videos => _videos.AsReadOnly();
        public IReadOnlyCollection<ValidationAttribute> ValidationAttributes => _validationAttributes.AsReadOnly();

        public Validation(Guid id,
                          string name,
                          string nameOnApplication,
                          string description,
                          string instruction,
                          BuildingBlockIcon icon) : base(id, BlockType.Validation)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(name)));
            }

            Name = name;
            NameOnApplication = nameOnApplication;
            Description = description;
            Instruction = instruction;
            Icon = icon;
            BlockType = BlockType.Validation;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }
        public void AddImage(Image image)
        {
            if (!_images.Contains(image))
            {
                _images.Add(image);
            }
        }

        public void RemoveImage(Image image)
        {
            if (_images.Contains(image))
            {
                _images.Remove(image);
            }
        }

        public void AddVideo(Video video)
        {
            if (!_videos.Contains(video))
            {
                _videos.Add(video);
            }
        }

        public void RemoveVideo(Video video)
        {
            if (_videos.Contains(video))
            {
                _videos.Remove(video);
            }
        }

        public void AddTag(Tag tag)
        {
            if (!_tags.Contains(tag))
            {
                _tags.Add(tag);
            }
        }

        public void RemoveTag(Tag tag)
        {
            if (_tags.Contains(tag))
            {
                _tags.Remove(tag);
            }
        }

        public void AddValidationAttribute(ValidationAttribute validationAttribute)
        {
            if (!_validationAttributes.Contains(validationAttribute))
            {
                _validationAttributes.Add(validationAttribute);
            }
        }

        public void RemoveValidationAttribute(ValidationAttribute validationAttribute)
        {
            if (_validationAttributes.Contains(validationAttribute))
            {
                _validationAttributes.Remove(validationAttribute);
            }
        }
    }
}
