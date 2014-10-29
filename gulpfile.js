/// <vs />
var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var del = require('del');
var watch = require('gulp-watch');
var gulpFilter = require('gulp-filter');
var mainBowerFiles = require('main-bower-files');
var minifyCss = require('gulp-minify-css');
var concatCss = require('gulp-concat-css');
var rename = require('gulp-rename');
var ts = require('gulp-typescript');

var config = {
    src : mainBowerFiles()
}

var jsFilter = gulpFilter('*.js');
var cssFilter = gulpFilter('*.css');
var fontFilter = gulpFilter(['*.eot', '*.woff', '*.svg', '*.ttf']);

gulp.task('clean', function() {
    del.sync(['ScoreTracker/app/all.min.js']);
});

gulp.task('scripts', ['clean'], function() {

    gulp.src(config.src)
        // JS
        .pipe(jsFilter)
        //.pipe(uglify())
        .pipe(concat('all.min.js'))
        .pipe(gulp.dest('ScoreTracker.Web/Scripts/'))
        .pipe(jsFilter.restore())

        // CSS
        .pipe(cssFilter)
        .pipe(gulp.dest('ScoreTracker.Web/Content/'))
        .pipe(minifyCss())
        //.pipe(concatCss("bundle.css"))
        //.pipe(gulp.dest('ScoreTracker.Web/Content/'))
        .pipe(cssFilter.restore());
});

//gulp.task('bower', function() {
//    return bower()
//        .pipe(gulp.dest(config.bowerDir));
//});

gulp.task('watch', function() {
    gulp.watch(config.src, ['scripts']);
});

gulp.task('default', ['scripts'], function() {});