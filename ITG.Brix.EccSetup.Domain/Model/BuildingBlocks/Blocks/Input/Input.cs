using ITG.Brix.EccSetup.Domain.Internal;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.Domain
{
    public class Input : BuildingBlock
    {
        private List<Tag> _tags = new List<Tag>();
        private List<Image> _images = new List<Image>();
        private List<Video> _videos = new List<Video>();
        private List<InputAttribute> _inputAttributes = new List<InputAttribute>();

        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid Icon { get; private set; }
        public string Instruction { get; private set; }
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
        public IReadOnlyCollection<Image> Images => _images.AsReadOnly();
        public IReadOnlyCollection<Video> Videos => _videos.AsReadOnly();
        public IReadOnlyCollection<InputAttribute> InputAttributes => _inputAttributes.AsReadOnly();

        public Input(Guid id, string name, string description, Guid icon, string instruction) : base(id, BlockType.Input)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(name)));
            }

            Name = name;
            Description = description;
            Icon = icon;
            Instruction = instruction;
        }

        public void ChangeName(string name)
        {
            Name = name;
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

        public void ClearTags() => _tags.Clear();

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

        public void ClearImages() => _images.Clear();

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

        public void ClearVideos() => _videos.Clear();

        public void AddInputAttribute(InputAttribute inputAttribute)
        {
            if (!_inputAttributes.Contains(inputAttribute))
            {
                _inputAttributes.Add(inputAttribute);
            }
        }

        public void RemoveInputAttribute(InputAttribute inputAttribute)
        {
            if (_inputAttributes.Contains(inputAttribute))
            {
                _inputAttributes.Remove(inputAttribute);
            }
        }

        public void ClearInputAttributes() => _inputAttributes.Clear();
    }
}
