<template>
  <div class="edit-photo">
    <h1>Edit photo</h1>
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
        @change="uploadImage"
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
import {
  postService,
  categoryService,
  fileService,
  signalRService,
} from "../../services";
import router from "../../router";
import store from "../../store";
import useAlert from "../../components/Alert/UseAlert";
import validate from "./validator";

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
    });

    watchEffect(async () => {
      const id = window.location.href.split("/")[4];
      const post = await postService.getById(id);
      const categories = await categoryService.get();

      state.categories = categories;
      state.id = post.id;
      state.description = post.description;

      if (post.location == "null") {
        state.location = "";
      } else {
        state.location = post.location;
      }

      if (post.categoryId == null) {
        state.categoryId = "";
      } else {
        state.categoryId = post.categoryId;
      }
    });

    const handleSubmit = async () => {
      const { id, location, description, categoryId } = state;
      const isCorrect = validate(state);

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

      if (response == 401) {
        router.push("/login");
      } else if (response >= 200 && response < 300) {
        useAlert("Successful!", true);
        signalRService.returnPosts();
        router.push("/" + state.id);
      } else {
        useAlert("Something went wrong!", false);
      }
    };

    const uploadImage = (e) => {
      const file = fileService.handleFileUpload(e);
      if (file) {
        state.picture = file;
        var fr = new FileReader();
        fr.readAsDataURL(file);
        fr.onload = () => {
          state.pictureBase64 = fr.result;
        };
      }
    };

    return {
      state,
      uploadImage,
      handleSubmit,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
