import { postService } from "../../services";

const getFilteredPosts = async (filter, pageCount) => {
  let posts = [];
  
  switch (filter) {
    case 1: {
      const topPosts = await postService.getTop5();
      posts = topPosts;
      break;
    }
    case 2: {
      const sortedPosts = await postService.getPostsSortedByDate(pageCount);
      posts = sortedPosts;
      break;
    }
    case 3: {
      const randomPosts = await postService.getRandomPosts(pageCount);
      posts = randomPosts;
      break;
    }
    default: {
      console.log(`No filter`);
    }
  }
  
  return posts;
};

export default getFilteredPosts;