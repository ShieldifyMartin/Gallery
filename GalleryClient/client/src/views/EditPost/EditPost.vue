<template>
  <div class="edit-photo">
    <h1>Edit photo</h1>
    <p class="error">{{state.error}}</p>
    <form method="post" @submit.prevent="handleSubmit">
      <label for="file" class="photo-upload-label">choose a picture</label>
      <input type="file" id="file" class="photo-upload" ref="picture" @change="handleFileUpload" />
      <input type="text" v-model="state.post.location" name="location" placeholder="Location" />
      <input
        type="text"        
        v-model="state.post.description"
        name="description"
        placeholder="Description"
        required
      />
      <select v-model="state.post.categoryId" class="categories-dropdown">
        <option value="" selected>Select category</option>
        <option
          v-for="category in state.categories"          
          :key="category.id"
          :value="category.id"
        >{{category.title}}</option>
      </select>
      <input type="submit" value="Edit" class="submit-btn" />
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import { postService } from "../../services";
import router from '../../router';
import store from '../../store';

export default defineComponent({
  setup() {
    const state = reactive({      
      categories: [],
      post: {},
      picture: null,      
      error: "",
      maxSize: 15728640,
    });    

    watchEffect(async () => {
      const id = window.location.href.split("/")[4];      
      const post = await postService.getById(id);
      const categories = await postService.getCategories();      

      state.categories = categories;
      state.post = post;      
    });

    function handleFileUpload(e) {
      let file = e.target.files[0];

      if (!e.target.files.length) return;

      if (e.target.files.length === 1) {
        if (file.size > state.maxSize) {
          state.error = "Too large picture!";
        } else {          
          state.picture = file;
        }
      } else {
        state.error = "Only one photo is allowed!";
      }
    }

    async function handleSubmit() {
      const { id, location, description, categoryId } = state.post;

      const isCorrect = validate();

      if (!isCorrect) {
        return;
      }

      var response = await postService.edit(
        store.state.auth.state.token,
        id,
        state.picture,
        location,
        description,
        categoryId
      );
 
      if (response === 401) {
        router.push("/login");
      } else if(response >= 200 && response < 300) {        
        router.push("/" + state.post.id);
      } else {
        state.error = "Something went wrong!";
      }
    }

    const validate = () => {
      if (state.post.description.length === 0) {
        state.error = "Invalid data!";
        return false;
      }

      if (state.post.description.length > 2000) {
        state.error = "Max description length is 2000!";
        return false;
      }

      if (state.post.location.length > 40) {
        state.error = "Max location length is 40!";
        return false;
      }

      return true;
    };

    return {
      state,      
      handleFileUpload,
      handleSubmit
    };
  }
});
</script>

<styles src="./index.scss"></styles>