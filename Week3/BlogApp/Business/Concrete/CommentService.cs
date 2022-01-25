using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Business.Exceptions;
using Data.EfCore;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class CommentService : ICommentService
    {
        private readonly EfCoreCommentRepository _commentRepository;
        private readonly EfCorePostRepository _postCepository;
        private readonly EfCoreUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CommentService(EfCoreCommentRepository repository,
            EfCorePostRepository postCepository,
            EfCoreUserRepository userRepository,
            IMapper mapper)
        {
            _commentRepository = repository;
            _postCepository = postCepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task CreateComment(CreateCommentDto model)
        {
            var userExisted = _userRepository.Get(x => x.Id == model.UserId);
            if (userExisted == null)
            {
                throw new BadRequestException("User not found");
            }

            var postExisted = _postCepository.Get(x => x.Id == model.PostId);
            if (postExisted == null)
            {
                throw new BadRequestException("Post not found");
            }

            var comment = _mapper.Map<Comment>(model);
            comment.CreatedAt = DateTime.Now;
            comment.Post = postExisted;
            comment.User = userExisted;
            await _commentRepository.Add(comment);
        }
    }
}
