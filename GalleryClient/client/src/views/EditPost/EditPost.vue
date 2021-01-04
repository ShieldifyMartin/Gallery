<template>
  <div class="edit-photo">
    <h1>Edit photo</h1>
    <p class="error">{{ state.error }}</p>
    <form method="post" @submit.prevent="handleSubmit">
      <img
        v-if="state.pictureBase64"
        :src="state.pictureBase64"
        class="uploaded-image"
        alt="upload"
      />
      <label for="file" class="upload-label">choose a picture</label>
      <input
        type="file"
        id="file"
        class="file"
        size="80"
        ref="picture"
        @change="handleFileUpload"
      />
      <input
        type="text"
        v-model="state.location"
        name="location"
        placeholder="Location"
      />
      <input
        type="text"
        v-model="state.description"
        name="description"
        placeholder="Description"
        required
      />
      <select v-model="state.categoryId" class="categories-dropdown">
        <option value="" selected>Select category</option>
        <option
          v-for="category in state.categories"
          :key="category.id"
          :value="category.id"
          >{{ category.title }}</option
        >
      </select>
      <input type="submit" value="Edit" class="submit-btn" />
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import { postService, categoryService } from "../../services";
import router from "../../router";
import store from "../../store";

export default defineComponent({
  setup() {
    const state = reactive({
      categories: [],
      picture: null,
      pictureBase64: null,
      id: null,
      location: "",
      description: "",
      categoryId: null,
      error: "",
      maxSize: 15728640,
    });

    watchEffect(async () => {
      const id = window.location.href.split("/")[4];
      const post = await postService.getById(id);
      console.log(post);
      const categories = await categoryService.get();

      state.categories = categories;
      state.id = post.id;
      state.description = post.description;      

      if (post.location == "null") {
        state.location = "";
      } else {
        state.location = post.location;
      }

      if(post.categoryId == null) {
        state.categoryId = "";
      } else {
        state.categoryId = post.categoryId;
      }
    });

    function handleFileUpload(e) {
      let file = e.target.files[0];

      if (!e.target.files.length) return;

      if (e.target.files.length === 1) {
        if (file.size > state.maxSize) {
          state.error = "Too large picture!";
        } else {
          state.picture = file;
          var fr = new FileReader();
          fr.readAsDataURL(file);
          fr.onload = () => {
            state.pictureBase64 = fr.result;
          };
        }
      } else {
        state.error = "Only one photo is allowed!";
      }
    }

    async function handleSubmit() {
      const { id, location, description, categoryId } = state;
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
      } else if (response >= 200 && response < 300) {
        router.push("/" + state.id);
      } else {
        state.error = "Something went wrong!";
      }
    }

    const validate = () => {
      if (state.description && state.description.length === 0) {
        state.error = "Invalid data!";
        return false;
      } else if (state.description && state.description.length > 2000) {
        state.error = "Max description length is 2000!";
        return false;
      }

      if (state.location && state.location.length > 40) {
        state.error = "Max location length is 40!";
        return false;
      }

      return true;
    };

    return {
      state,
      handleFileUpload,
      handleSubmit,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
